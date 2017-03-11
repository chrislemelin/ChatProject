using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

namespace ChatServer
{
	public class ClientProxy : ServerListener
	{
		private Socket client;

		public ClientProxy(Socket client)
		{
			this.client = client;
		}

		public void authenticated()
		{



			Send(Resources.s_authenicated + "");
			Console.WriteLine("authenicated user");
		}

		public void updateLobby(string update)
		{
			throw new NotImplementedException();
		}

		public void updateRoom(string update)
		{
			throw new NotImplementedException();
		}

		private void Send(String data)
		{
			// Convert the string data to byte data using ASCII encoding.  
			byte[] byteData = Encoding.ASCII.GetBytes(data + Reader.EOM);

			// Begin sending the data to the remote device.  
			client.BeginSend(byteData, 0, byteData.Length, 0,
				new AsyncCallback(SendCallback), client);
		}

		private void SendCallback(IAsyncResult ar)
		{
			try
			{
				// Complete sending the data to the remote device.  
				int bytesSent = client.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to server.", bytesSent);

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

	}
}
