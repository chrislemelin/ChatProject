﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using Google.Protobuf;

namespace ChatServer
{
	public class ClientProxy : ServerListener
	{
		private Socket client;
		public ClientModel clientModel { get;}

		public ClientProxy(Socket client)
		{
			this.client = client;
		}

		public void authenticated(bool success)
		{
			SCMessageWrapper wrapper = new SCMessageWrapper();
			Authenticated auth = new Authenticated();
			auth.Success = success;
			wrapper.Authenticated = auth;
			Send(wrapper);
		}

		public void registerResponse(bool success)
		{
			SCMessageWrapper wrapper = new SCMessageWrapper();
			RegisterResponse reg = new RegisterResponse();
			reg.Success = success;
			wrapper.RegisterResponse = reg;
			Send(wrapper);
		}

		public void makeRoomResponse(bool success)
		{
			SCMessageWrapper wrapper = new SCMessageWrapper();
			MakeRoomResponse reg = new MakeRoomResponse ();
			reg.Success = success;
			wrapper.MakeRoomResponse = reg;
			Send(wrapper);
		}

		public void updateLobby(List<UpdateLobbyPiece> pieces)
		{
			UpdateLobby update = new UpdateLobby();
			foreach (UpdateLobbyPiece piece in pieces)
			{
				update.UpdateLobbyPieces.Add(piece);
			}
			SCMessageWrapper wrapper = new SCMessageWrapper();
			wrapper.UpdateLobby = update;
			Send(wrapper);

		}

		public void updateRoom(List<UpdateRoomPiece> pieces)
		{
			UpdateRoom update = new UpdateRoom();
			foreach (UpdateRoomPiece piece in pieces)
			{
				update.UpdageRoomPieces.Add(piece);
			}
			SCMessageWrapper wrapper = new SCMessageWrapper();
			wrapper.UpdateRoom = update;
			Send(wrapper);
		}

		private void Send(SCMessageWrapper wrapper)
		{ 
			byte[] data = wrapper.ToByteArray();
			byte[] length;
			length = BitConverter.GetBytes(data.Length);
			Console.WriteLine(data.Length);

			// Begin sending the data to the remote device. 
			client.BeginSend(length, 0, 4, 0,
				new AsyncCallback(SendCallback), client);
			client.BeginSend(data, 0, data.Length, 0,
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
