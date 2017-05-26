using System;
namespace ChatServer
{
	public class MessageModel
	{
		public UserDB author;
		public String messageText;
		public DateTime timeStamp;


		public MessageModel()
		{
		}

		public String toString()
		{
			return author + " : " +messageText+ "\n";
		}

	}
}
