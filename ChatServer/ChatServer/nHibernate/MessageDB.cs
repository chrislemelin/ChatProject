using System;
namespace ChatServer
{
	public class MessageDB
	{
		public virtual int ID { get; set; }

		public virtual DateTime TimeStamp { get; set; }

		public virtual UserDB Author { get; set; }

		public virtual String MessageText { get; set; }

		public MessageDB()
		{
		}

	}
}
