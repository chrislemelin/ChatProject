using System;
using System.Collections.Generic;
using NHibernate;
using Google.Protobuf.WellKnownTypes;
namespace ChatServer
{
	public class Model
	{
		public Dictionary<int,RoomModel> rooms= new Dictionary<int, RoomModel>();
		//public List<RoomModel> rooms= new List<RoomModel>();

		private int roomIdCounter = 1;
		private List<ClientProxy> proxies = new List<ClientProxy>();


		public Model()
		{
			using (ISession session = nHibernateResources.Factory.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					Room room1 = new Room { Title = "testTitle"};
					session.Save(room1);
					transaction.Commit();

				}
			}

			RoomModel room = new RoomModel();
			room.title = "first";
			room.id = roomIdCounter++;
			rooms.Add(room.id,room);

			MessageModel m = new MessageModel();

			RoomModel room2 = new RoomModel();
			room2.title = "second";
			room2.id = roomIdCounter++;
			rooms.Add(room2.id, room2);

		}

		public void addProxy(ClientProxy p)
		{
			proxies.Add(p);
		}

		public bool removeProxy(ClientProxy p)
		{
			foreach(RoomModel room in rooms.Values)
			{
				room.subs.Remove(p);
			}
			return proxies.Remove(p);
		}

		public void initLobby(ClientProxy proxy)
		{
			List<UpdateLobbyPiece> pieces = new List<UpdateLobbyPiece>();
			foreach (RoomModel room in rooms.Values)
			{
				UpdateLobbyPiece piece = new UpdateLobbyPiece();
				piece.Title = room.title;
				piece.Id = room.id;
				pieces.Add(piece);
			}			
			proxy.updateLobby(pieces);
		}

		public User login(String username, int password, ClientProxy proxy)
		{
			username = username.Trim();
			using (ISession session = nHibernateResources.Factory.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					User user = nHibernateResources.Get<User, String>("Username", username, session);
					if (user != null)
					{
						if (user.Password.Equals(password))
						{
							return user;
						}
						return null;
					}
					else
					{
						return null;
					}
				}
			}
		}

		public User addUser(String username,int password, ClientProxy proxy)
		{
			username = username.Trim();

			using (ISession session = nHibernateResources.Factory.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					if (nHibernateResources.Get<User, String>("Username", username, session) == null)
					{
						User user = new User { Username = username, Password = password };
						session.Save(user);
						transaction.Commit();
						return user;
					}
					else
					{
						return null;
					}
				}
			}

		}

		public User removeUser(string username)
		{
			using (ISession session = nHibernateResources.Factory.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					User user = nHibernateResources.Get<User, String>("Username", username, session);
					if (user != null)
					{
						session.Delete(user);
						transaction.Commit();
						return user;
					}
					else
					{
						return null;
					}
				}
			}
		}

		public void addRoom(string title, ClientProxy client)
		{
			RoomModel newRoomModel = new RoomModel();
			newRoomModel.title = title;
			newRoomModel.id = roomIdCounter++;
			rooms.Add(newRoomModel.id, newRoomModel);

			List<UpdateLobbyPiece> pieces = new List<UpdateLobbyPiece>();
			UpdateLobbyPiece piece = new UpdateLobbyPiece();
			pieces.Add(piece);
			piece.Id = newRoomModel.id;
			piece.Title = newRoomModel.title;

			client.makeRoomResponse(true);

			foreach (ClientProxy proxy in proxies)
			{
				proxy.updateLobby(pieces);
			}
		}

		public void subscribe(int id, ClientProxy client)
		{
			
			RoomModel room;
			if (rooms.TryGetValue(id, out room))
			{
				if (!room.subs.Contains(client))
				{
					room.subs.Add(client);
					initRoom(rooms[id], client);

				}
			}
		}

		public void unsubscribe(int id, ClientProxy client)
		{
			rooms[id].subs.Remove(client);
		}

		public void addMessage(int id, ClientProxy client, User author,String message)
		{
			DateTime now = DateTime.Now;
			now = now.ToUniversalTime();
			RoomModel room = rooms[id];
			MessageModel newMessage = new MessageModel();
			newMessage.author = author;
			newMessage.messageText = message;
			newMessage.timeStamp = now;
			room.messages.Add(newMessage);
			foreach (ClientProxy subscriber in rooms[id].subs)
			{
				UpdateRoomPiece p = new UpdateRoomPiece();
				p.Author = author.Username;
				p.MessageText = message;
				//p.Time = new Timestamp();
				p.Time = Timestamp.FromDateTime(now);


				List<UpdateRoomPiece> pieces = new List<UpdateRoomPiece>();
				pieces.Add(p);
				subscriber.updateRoom(pieces);
			}

		}

		private void updateSubscriber(RoomModel room, MessageModel message)
		{
			List<UpdateRoomPiece> pieces = new List<UpdateRoomPiece>();
			UpdateRoomPiece p = new UpdateRoomPiece();
			pieces.Add(p);
			p.Author = message.author.Username;
			p.MessageText = message.messageText;
			Timestamp newTime = new Timestamp();
			newTime.Seconds = message.timeStamp.ToFileTimeUtc();
			p.Time = newTime;

			foreach (ClientProxy client in room.subs)
			{
				client.updateRoom(pieces);
			}
		}

		public void initRoom(RoomModel room, ClientProxy client)
		{
			List<UpdateRoomPiece> pieces = new List<UpdateRoomPiece>();

			foreach(MessageModel m in room.messages)
			{
				UpdateRoomPiece p = new UpdateRoomPiece();
				p.Author = m.author.Username;
				p.MessageText = m.messageText;
				p.Time = Timestamp.FromDateTime(m.timeStamp.ToUniversalTime());
				pieces.Add(p);
			}
			client.updateRoom(pieces);
		}


	}
}
