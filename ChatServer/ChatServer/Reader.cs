﻿using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using Google.Protobuf;

namespace ChatServer
{
	public class Reader
	{
		public static readonly char EOM = (char)10;
		public static readonly char EOD = (char)11;
		public Socket socket;
		public ClientProxy client;
		public Model model;
		public ClientModel clientModel = null;
		public User user = null;

		private StringBuilder sb = new StringBuilder();

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
				//model.removeUser(clientModel.id);

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
				User newUser = model.AddUser(wrapper.Register.Username, wrapper.Register.Password1,client);
				if (newUser != null)
				{
					client.registerResponse(true);
				}
				else
				{
					client.registerResponse(false);
				}

			}

			if (wrapper.Login != null && user == null)
			{
				user = model.Login(wrapper.Login.Name, wrapper.Login.Password, client);
				if (user != null)
				{
					client.authenticated(true);
				}
				else
				{
					client.authenticated(false);
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
