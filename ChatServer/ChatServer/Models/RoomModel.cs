using System;
using System.Collections.Generic;
namespace ChatServer
{
	public class RoomModel
	{
		public string title;
		public int id;
		public List<ClientProxy> subs = new List<ClientProxy>();
		public List<MessageModel> messages = new List<MessageModel>();

		public RoomModel()
		{
		}
	}
}
