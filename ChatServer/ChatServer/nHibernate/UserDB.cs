using System;
using NHibernate;
using System.Collections.Generic;
namespace ChatServer
{
	public class UserDB
	{

		public virtual int ID { get; set; }

		public virtual string Username { get; set; }

		public virtual int Password { get; set; }

		public virtual IList<RoomDB> OwnedRooms { get; set;}


	}
}
