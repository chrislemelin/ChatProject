using System;
using System.Collections.Generic;
namespace ChatServer
{
	public class RoomModel
	{
		public List<ClientProxy> subs = new List<ClientProxy>();
		public List<MessageModel> messages = new List<MessageModel>();
		public RoomDB room;

		public RoomModel(RoomDB room)
		{
			this.room = room;
		}
	}
}
