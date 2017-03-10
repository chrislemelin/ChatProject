using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;

namespace ChatServer
{
	public class Reader
	{
		public static readonly char EOM = (char)10;
		public static readonly char EOD = (char)11;
		public Socket socket;
		public ClientProxy client;
		public Model model;
		private StringBuilder sb = new StringBuilder();
		private String response = String.Empty;

		public void Start()
		{
			Thread.CurrentThread.IsBackground = true;
			while (SocketConnected(socket))
			{
				Receive(socket);
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

				while (bytes[bytesRead - 1] != (byte)EOM)
				{
					// There might be more data, so store the data received so far.  
					bytesRead = client.Receive(bytes);
					sb.Append(Encoding.ASCII.GetString(bytes, 0, bytesRead));
					response = sb.ToString();
					//Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));
				}
				response = sb.ToString();
				Console.WriteLine("--" + response + "--");
				translate(response);

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

		private void translate(String message)
		{
			if (message[0] == Resources.c_login)
			{
				if (model.addUser(message.Substring(1), client))
				{
					client.authenticated();
				}
			}
		}
	}
}
