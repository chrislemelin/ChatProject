﻿using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;

namespace ChatServer
{
	public class Reader
	{
		public static readonly char EOM = (char)10;
		public static readonly char EOD = (char)11;
		public Socket client;
		private StringBuilder sb = new StringBuilder();
		private String response = String.Empty;

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


	public class ConnectionListener
	{
		// Thread signal.  
		public ManualResetEvent allDone = new ManualResetEvent(false);
		public static readonly char EOM = (char)10;




		public ConnectionListener()
		{
		}

		public void StartListening()
		{
			// Data buffer for incoming data.  

			// Establish the local endpoint for the socket.  
			// The DNS name of the computer  
			// running the listener is "host.contoso.com".  
			IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");;
			IPAddress ipAddress = ipHostInfo.AddressList[0];
			IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 10000);

			// Create a TCP/IP socket.  
			Socket listener = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and listen for incoming connections.  
			try
			{
				listener.Bind(localEndPoint);
				listener.Listen(100);

				while (true)
				{
					// Set the event to nonsignaled state.  
					allDone.Reset();

					// Start an asynchronous socket to listen for connections.  
					Console.WriteLine("Waiting for a connection...");
					listener.BeginAccept(
						new AsyncCallback(AcceptCallback),
						listener);

					// Wait until a connection is made before continuing.  
					allDone.WaitOne();
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();

		}

		public void AcceptCallback(IAsyncResult ar)
		{
			// Signal the main thread to continue.  
			allDone.Set();

			// Get the socket that handles the client request.  
			Socket listener = (Socket)ar.AsyncState;
			Socket handler = listener.EndAccept(ar);

			// Create the state object.  
			ClientProxy state = new ClientProxy();
			state.workSocket = handler;

			Console.WriteLine("making thread");
			Reader rd = new Reader();
			rd.client = state.workSocket;
			Thread oThread = new Thread(new ThreadStart(rd.Start));
			oThread.Start();

			//handler.BeginReceive(state.buffer, 0, ClientProxy.BufferSize, 0,
			//	new AsyncCallback(ReadCallback), state);
		}

		public void ReadCallback(IAsyncResult ar)
		{
			String content = String.Empty;

			// Retrieve the state object and the handler socket  
			// from the asynchronous state object.  
			ClientProxy state = (ClientProxy)ar.AsyncState;
			Socket handler = state.workSocket;

			// Read data from the client socket.   
			int bytesRead = handler.EndReceive(ar);

			if (bytesRead > 0)
			{
				// There  might be more data, so store the data received so far.  
				state.sb.Append(Encoding.ASCII.GetString(
					state.buffer, 0, bytesRead));

				// Check for end-of-file tag. If it is not there, read   
				// more data.  
				content = state.sb.ToString();
				if (content.IndexOf(EOM) > -1)
				{
					// All the data has been read from the   
					// client. Display it on the console.  
					Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
						content.Length, content);
					// Echo the data back to the client.  
					Send(handler, content);
				}
				else {
					// Not all data received. Get more.  
					handler.BeginReceive(state.buffer, 0, ClientProxy.BufferSize, 0,
					new AsyncCallback(ReadCallback), state);
				}
			}
		}

		private static void Send(Socket handler, String data)
		{
			// Convert the string data to byte data using ASCII encoding.  
			byte[] byteData = Encoding.ASCII.GetBytes(data+EOM);

			// Begin sending the data to the remote device.  
			handler.BeginSend(byteData, 0, byteData.Length, 0,
				new AsyncCallback(SendCallback), handler);
				handler.BeginSend(byteData, 0, byteData.Length, 0,
			new AsyncCallback(SendCallback), handler);
				handler.BeginSend(byteData, 0, byteData.Length, 0,
			new AsyncCallback(SendCallback), handler);
		
		}

		private static void SendCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.  
				Socket handler = (Socket)ar.AsyncState;

				// Complete sending the data to the remote device.  
				int bytesSent = handler.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to client.", bytesSent);

				//handler.Shutdown(SocketShutdown.Both);
				//handler.Close();

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}
	}
}
