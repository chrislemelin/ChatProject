using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using Google.Protobuf;

namespace ChatServer
{
	public class Reader
	{
		public Socket socket;
		public ClientProxy proxy;
		public Model model;
		public ClientModel clientModel = null;
		public User user = null;

		public void Start()
		{
			Thread.CurrentThread.IsBackground = true;
			while (SocketConnected(socket))
			{
				Receive(socket);
			}

			//disconected
			if (clientModel != null)
			{
				model.removeProxy(proxy);
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

				CSMessageWrapper message = CSMessageWrapper.Parser.ParseFrom(data);
				ProccessMessage(message);
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

		private void ProccessMessage(CSMessageWrapper wrapper)
		{
			if (wrapper.Register != null)
			{
				User newUser = model.AddUser(wrapper.Register.Username, wrapper.Register.Password1,proxy);
				if (newUser != null)
				{
					proxy.registerResponse(true);
				}
				else
				{
					proxy.registerResponse(false);
				}
			}

			if (wrapper.Login != null && user == null)
			{
				user = model.Login(wrapper.Login.Name, wrapper.Login.Password, proxy);
				if (user != null)
				{
					proxy.authenticated(true);
					model.initLobby(proxy);
				}
				else
				{
					proxy.authenticated(false);
				}
			}
			else
			{
				// user is already logged in
			}


			if (wrapper.MakeRoom != null)
			{
				// add make room logic
			}
			if (wrapper.JoinLobby != null)
			{
				// add joining room logic
			}
			if (wrapper.SendMessage != null)
			{
				// add posting message logic	
			}
		}
	}
}
