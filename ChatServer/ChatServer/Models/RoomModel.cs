using System;
using System.Collections.Generic;
namespace ChatServer
{
	public class RoomModel
	{
		public string title;
		public int id;
		public List<ClientModel> inRoom;


		public RoomModel()
		{
		}
	}
}
