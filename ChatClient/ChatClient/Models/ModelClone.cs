using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gtk;

namespace ChatClient
{
	public class ModelClone
	{
		public LobbyWindow lobbyWindow = null;
		public LoginWindow loginWindow = null;

		//private Map<EventBox, RoomClone> rooms = new Map<EventBox, RoomClone>();
		public List<RoomClone> rooms = new List<RoomClone>();


		public ModelClone()
		{
		}

		public void addRoom(int id, string title)
		{
			RoomClone room = new RoomClone(id);
			room.Title = title;
			rooms.Add(room);
			if (lobbyWindow != null)
			{
				lobbyWindow.addLabel(room.Title);
			}
		}

	}
}
