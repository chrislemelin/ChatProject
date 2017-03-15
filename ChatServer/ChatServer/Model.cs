using System;
using System.Collections.Generic;
using NHibernate;
namespace ChatServer
{
	public class Model
	{
		//List<RoomModel>

		public Model()
		{
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
	}
}
