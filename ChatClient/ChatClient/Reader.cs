using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

namespace ChatClient
{
	public class Reader
	{
		public Socket client;
		public LoginWindow loginWindow;
		public RegisterWindow registerWindow;
		public MakeRoomWindow makeRoomWindow;
		public RoomWindow roomWindow;


		private ModelClone modelClone;

		public Reader(ModelClone modelClone)
		{
			this.modelClone = modelClone;
		}

		public void Start()
		{
			Thread.CurrentThread.IsBackground = true;
			while (SocketConnected(client))
			{
				Receive(client);
			}
		}

		private void Receive(Socket client)
		{
			try
			{
				byte[] length = new byte[4];
				client.Receive(length);
				int len = BitConverter.ToInt32(length, 0);
				byte[] data = new byte[len];
				client.Receive(data);

				SCMessageWrapper message = SCMessageWrapper.Parser.ParseFrom(data);
				processMessage(message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		private void processMessage(SCMessageWrapper wrapper)
		{
			if (wrapper.RegisterResponse != null && loginWindow != null)
			{
				if (wrapper.RegisterResponse.Success)
				{
					registerWindow.DisplayMessage("registration successful");
				}
				else
				{
					registerWindow.DisplayMessage("username already taken");
				}
			}

			if (wrapper.Authenticated != null)
			{
				if (wrapper.Authenticated.Success)
				{
					loginWindow.DisplayMessage("login was a success");
					loginWindow.StartLobbyWindow();

				}
				else
				{
					loginWindow.DisplayMessage("login failed");
				}
			}

			if (wrapper.UpdateLobby != null)
			{
				UpdateLobby updateLobby = wrapper.UpdateLobby;
				foreach (UpdateLobbyPiece piece in updateLobby.UpdateLobbyPieces)
				{
					if (piece.Type == UpdateLobbyPiece.Types.Type.Add)
					{
						modelClone.addRoom(piece.Id, piece.Title);

					}
					if (piece.Type == UpdateLobbyPiece.Types.Type.Delete)
					{
						//remove room
					}
					if (piece.Type == UpdateLobbyPiece.Types.Type.Modify)
					{
						//update room
					}
				}
			}

			if (wrapper.MakeRoomResponse != null)
			{
				if (makeRoomWindow != null)
				{
					if (wrapper.MakeRoomResponse.Success)
						makeRoomWindow.DisplayMessage("Success");
					else
						makeRoomWindow.DisplayMessage("Failure");
				}
			}

			if (wrapper.UpdateLobby != null)
			{
				// updateLoby
			}
			if (wrapper.UpdateRoom != null && roomWindow != null)
			{
				foreach (UpdateRoomPiece p in wrapper.UpdateRoom.UpdageRoomPieces)
				{
					MessageClone ms = new MessageClone();
					ms.author = p.Author;
					ms.message = p.MessageText;
					ms.timeStamp = p.Time.ToDateTime();
					roomWindow.addMessage(ms);
				}
			}
		}

		private bool SocketConnected(Socket s)
		{
			bool part1 = s.Poll(1000, SelectMode.SelectRead);
			bool part2 = (s.Available == 0);
			if (part1 && part2)
				return false;
			else
				return true;
		}
	}

	
}
