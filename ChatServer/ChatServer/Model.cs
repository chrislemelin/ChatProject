using System;
using System.Collections.Generic;
using NHibernate;
namespace ChatServer
{
	public class Model
	{
		//private List<IRoom> rooms;
		//private Dictionary<ClientProxy, ClientModel> clients_proxy;
		private Dictionary<String, ClientModel> clients_name;
		private Dictionary<int, ClientModel> clients_id;

		//private IRoom lobby;


		public Model()
		{
			clients_name = new Dictionary<string, ClientModel>();
			clients_id = new Dictionary<int, ClientModel>();
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
						if (user.Password == password)
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




			//makes sure there isn't already a user with the given username
			/*
			if (!clients_name.ContainsKey(username.Trim()))
			{
				ClientModel client = new ClientModel(username.Trim(),clientsIdCounter++,proxy);
				clients_name.Add(client.username, client);
				clients_id.Add(client.id, client);
				return client;
			}
			else
			{
				return null;
			}
			*/
		}

		public ClientModel removeUser(int id)
		{
			
			if (clients_id.ContainsKey(id))
			{

				ClientModel client = null;
				clients_id.TryGetValue(id,out client);
				clients_name.Remove(client.username);
				clients_id.Remove(client.id);
				return client;
			}
			else
			{
				return null;
			}
		}
	}
}
