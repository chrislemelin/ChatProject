using System;
using System.Collections.Generic;
using NHibernate.Linq;
using NHibernate;
using Google.Protobuf.WellKnownTypes;
namespace ChatServer
{
	public class Model
	{
		public Dictionary<int,RoomModel> rooms= new Dictionary<int, RoomModel>();
		private List<ClientProxy> proxies = new List<ClientProxy>();

		public Model()
		{

			using (ISession session = nHibernateResources.Factory.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					IEnumerable<RoomDB> allRooms = session.Query<RoomDB>().ToFuture();
					IEnumerable<MessageDB> allMessages = session.Query<MessageDB>().ToFuture();

					foreach (RoomDB currentRoom in allRooms)
					{
						RoomModel currentRoomModel = new RoomModel(currentRoom);
						rooms.Add(currentRoom.ID, currentRoomModel);
					}
				}
			}
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
				piece.Title = room.room.Title;
				piece.Id = room.room.ID;
				pieces.Add(piece);
			}			
			proxy.updateLobby(pieces);
		}

		public UserDB login(String username, int password, ClientProxy proxy)
		{
			username = username.Trim();
			using (ISession session = nHibernateResources.Factory.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					UserDB user = nHibernateResources.Get<UserDB, String>("Username", username, session);
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

		public UserDB addUser(String username,int password, ClientProxy proxy)
		{
			username = username.Trim();

			using (ISession session = nHibernateResources.Factory.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					if (nHibernateResources.Get<UserDB, String>("Username", username, session) == null)
					{
						UserDB user = new UserDB { Username = username, Password = password };
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

		public UserDB removeUser(string username)
		{
			using (ISession session = nHibernateResources.Factory.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					UserDB user = nHibernateResources.Get<UserDB, String>("Username", username, session);
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
			RoomDB newRoom;
			using (ISession session = nHibernateResources.Factory.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					newRoom = new RoomDB();
					newRoom.Owner = null;
					newRoom.Title = title;

					session.Save(newRoom);
					transaction.Commit();
				}
			}
		

			RoomModel newRoomModel = new RoomModel(newRoom);
			rooms.Add(newRoomModel.room.ID, newRoomModel);

			client.makeRoomResponse(true);
			updateLobby(newRoomModel);



		}

		public void subscribe(int id, bool subbing, ClientProxy client)
		{
			RoomModel room;
			if (rooms.TryGetValue(id, out room))
			{

				if (subbing && !room.subs.Contains(client))
				{
					room.subs.Add(client);
					initRoom(rooms[id], client);
				}
				else if (!subbing && room.subs.Contains(client))
				{
					room.subs.Remove(client);
				}
			}
		}

		public void unsubscribe(int id, ClientProxy client)
		{
			rooms[id].subs.Remove(client);
		}

		public void addMessage(int id, ClientProxy client, UserDB author,String message)
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
				p.RoomID = id;
				p.Author = author.Username;
				p.MessageText = message;
				p.Time = Timestamp.FromDateTime(now);

				List<UpdateRoomPiece> pieces = new List<UpdateRoomPiece>();
				pieces.Add(p);
				subscriber.updateRoom(pieces);
			}

		}

		//updates all clients that a new room was created
		private void updateLobby(RoomModel room)
		{
			List<UpdateLobbyPiece> pieces = new List<UpdateLobbyPiece>();
			UpdateLobbyPiece piece = new UpdateLobbyPiece();
			pieces.Add(piece);
			piece.Id = room.room.ID;
			piece.Title = room.room.Title;

			foreach (ClientProxy proxy in proxies)
			{
				proxy.updateLobby(pieces);
			}
		}

		//updates the subscribers of a room when a new message is sent
		private void updateSubscriber(RoomModel room, MessageModel message)
		{
			List<UpdateRoomPiece> pieces = new List<UpdateRoomPiece>();
			UpdateRoomPiece p = new UpdateRoomPiece();
			pieces.Add(p);
			p.RoomID = room.room.ID;
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

		//inits room when the client opens a new room in a window
		private void initRoom(RoomModel room, ClientProxy client)
		{
			List<UpdateRoomPiece> pieces = new List<UpdateRoomPiece>();

			foreach(MessageModel m in room.messages)
			{
				UpdateRoomPiece p = new UpdateRoomPiece();
				p.RoomID = room.room.ID;
				p.Author = m.author.Username;
				p.MessageText = m.messageText;
				p.Time = Timestamp.FromDateTime(m.timeStamp.ToUniversalTime());
				pieces.Add(p);
			}
			client.updateRoom(pieces);
		}


	}
}
