using System;
using NHibernate;
namespace ChatServer
{
	public class Room
	{

		public virtual int ID { get; set; }

		public virtual string Title { get; set; }

		public virtual int? Owner { get; set; }

		public Room()
		{

		}
	}
}