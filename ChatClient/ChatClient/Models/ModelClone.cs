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

		public Dictionary<int, RoomClone> rooms = new Dictionary<int, RoomClone>();

		public ModelClone()
		{
		}

		public void addRoom(int id, string title)
		{
			RoomClone room = new RoomClone(id);
			room.Title = title;
			rooms.Add(id ,room);
			if (lobbyWindow != null)
			{
				lobbyWindow.addRoom(room);
			}
		}

		public void removeRoom(int id)
		{
			rooms.Remove(id);
		}

	}
}
