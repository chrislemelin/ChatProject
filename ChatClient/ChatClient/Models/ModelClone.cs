using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gtk;

namespace ChatClient
{
	public class ModelClone
	{
		public LobbyWindow lobbyWindow;

		private Map<EventBox, RoomClone> rooms =
			new Map<EventBox, RoomClone>();

		public ModelClone()
		{
		}

		public async void addRoom(int id, string title)
		{
			RoomClone room = new RoomClone(id);
			room.Title = title;
			EventBox box = await(Task.Run(()=> lobbyWindow.addLabel(title)));
			rooms.Add(box,room);
		}

		public void joinLobby(EventBox e)
		{
			RoomClone room;
			room = rooms.Forward[e];
			Console.WriteLine(room.Title);
		}


	}
}
