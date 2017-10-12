using System;
using NHibernate;
namespace ChatServer
{
	public class RoomDB
	{

		public virtual int ID { get; set; }

		public virtual string Title { get; set; }

		public virtual UserDB Owner { get; set; }


	}
}