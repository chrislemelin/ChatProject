using System;
namespace ChatClient
{
	public class MessageClone
	{
		public string author;
		public string message;
		public DateTime timeStamp;

		public MessageClone()
		{
		}

		public String ToString()
		{
			return author + " : " + message +"\n sent at: "+timeStamp.ToString();
		}
	}
}
