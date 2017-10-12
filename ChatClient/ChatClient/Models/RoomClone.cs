using System;
using System.Collections.Generic;
namespace ChatClient
{
	public class RoomClone
	{
		public string Title { get; set; }
		public List<MessageClone> messages = new List<MessageClone>();
		public int ID
		{ 
			get{return id;}
		}

		public RoomWindow window;

		private int id;
		public RoomClone(int id)
		{
			this.id = id;
		}






	}
}
