using System;
using System.Collections.Generic;
namespace ChatServer
{
	public class RoomModel
	{
		public List<ClientProxy> subs = new List<ClientProxy>();
		public List<MessageModel> messages = new List<MessageModel>();
		public Room room;

		public RoomModel(Room room)
		{
			this.room = room;
		}
	}
}
