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
		private StringBuilder sb = new StringBuilder();
		private String response = String.Empty;
		public MessageInterperter interpreter = new MessageInterperter();


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
				sb.Clear();
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
				Console.WriteLine(e.Message);
			}
		}

		private void processMessage(SCMessageWrapper wrapper)
		{
			if (wrapper.Authenticated != null)
			{
				if(wrapper.Authenticated.Success)
					loginWindow.DisplayMessage("login was a success");
				else
					loginWindow.DisplayMessage("username already taken");
			}
			if (wrapper.UpdateLobby != null)
			{
				// updateLoby
			}
			if (wrapper.UpdateRoom != null)
			{
				//update room
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
