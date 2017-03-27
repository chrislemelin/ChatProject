using System;
using System.Collections.Generic;
using NHibernate;
namespace ChatServer
{
	public class Model
	{
		public List<RoomModel> lobby = new List<RoomModel>();
		int roomIdCounter = 1;
		List<ClientProxy> proxies = new List<ClientProxy>();

		public Model()
		{
			RoomModel room = new RoomModel();
			room.title = "first";
			room.id = roomIdCounter++;
			lobby.Add(room);

			RoomModel room2 = new RoomModel();
			room2.title = "second";
			room2.id = roomIdCounter++;
			lobby.Add(room2);

		}

		public void addProxy(ClientProxy p)
		{
			proxies.Add(p);
		}

		public bool removeProxy(ClientProxy p)
		{
			return proxies.Remove(p);
		}

		public void initLobby(ClientProxy proxy)
		{
			List<UpdateLobbyPiece> pieces = new List<UpdateLobbyPiece>();
			foreach (RoomModel room in lobby)
			{
				UpdateLobbyPiece piece = new UpdateLobbyPiece();
				piece.Title = room.title;
				piece.Id = room.id;
				pieces.Add(piece);
			}			
			proxy.updateLobby(pieces);
		}

		public User Login(String username, int password, ClientProxy proxy)
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

		public User AddUser(String username,int password, ClientProxy proxy)
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

		public User RemoveUser(string username)
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

		public void AddRoom(string title)
		{
			RoomModel newRoomModel = new RoomModel();
			newRoomModel.title = title;
			newRoomModel.id = roomIdCounter++;
			lobby.Add(newRoomModel);

			List<UpdateLobbyPiece> pieces = new List<UpdateLobbyPiece>();
			UpdateLobbyPiece piece = new UpdateLobbyPiece();
			pieces.Add(piece);
			piece.Id = newRoomModel.id;
			piece.Title = newRoomModel.title;

			foreach (ClientProxy proxy in proxies)
			{
				proxy.updateLobby(pieces);
			}
		}
	}
}
