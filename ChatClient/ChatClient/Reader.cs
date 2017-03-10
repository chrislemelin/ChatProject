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
				byte[] bytes = new byte[256];

				int bytesRead = client.Receive(bytes);

				sb.Append(Encoding.ASCII.GetString(bytes, 0, bytesRead));
				response = sb.ToString();

				while (bytes[bytesRead - 1] != (byte)Resources.EOM)
				{
					// There might be more data, so store the data received so far.  
					bytesRead = client.Receive(bytes);
					sb.Append(Encoding.ASCII.GetString(bytes, 0, bytesRead));
					response = sb.ToString();
				}
				response = sb.ToString();
				Console.WriteLine("--" + response + "--");
				//interpreter.interpret(response);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
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
