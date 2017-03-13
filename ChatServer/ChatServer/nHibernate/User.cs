using System;
using NHibernate;
namespace ChatServer
{
	public class User
	{

		public virtual int ID { get; set; }

		public virtual string Username { get; set; }

		public virtual int Password { get; set; }

		public User()
		{
		}
	}
}
