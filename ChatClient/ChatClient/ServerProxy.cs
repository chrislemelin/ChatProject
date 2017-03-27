using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;
using Google.Protobuf;
using System.Collections.Generic;

namespace ChatClient
{
	public class ServerProxy : ClientListener
	{
		// The port number for the remote device.  
		private const int port = 10000;

		// ManualResetEvent instances signal completion.  
		private ManualResetEvent connectDone =
			new ManualResetEvent(false);

		private ManualResetEvent sendDone =
			new ManualResetEvent(false);
		private Socket client;
		private ModelClone modelClone;

		public LoginWindow loginWindow;
		public Reader reader;


		public ServerProxy(ModelClone modelClone)
		{
			this.modelClone = modelClone;
		}

		public void StartClient()
		{
			// Connect to a remote device.  
			try
			{
				// Establish the remote endpoint for the socket.  
				IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
				IPAddress ipAddress = ipHostInfo.AddressList[0];
				IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

				// Create a TCP/IP socket.  
				client = new Socket(AddressFamily.InterNetwork,
					SocketType.Stream, ProtocolType.Tcp);

				// Connect to the remote endpoint.  
				client.BeginConnect(remoteEP,
					new AsyncCallback(ConnectCallback), client);
				connectDone.WaitOne();

				// Send test data to the remote device. 

				Reader rd = new Reader(modelClone);
				rd.loginWindow = loginWindow;
				rd.client = client;
				loginWindow.rd = rd;
				Thread oThread = new Thread(new ThreadStart(rd.Start));
				oThread.Start();
				 

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Environment.Exit(0);
			}
		}


		private void ConnectCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.  
				Socket client = (Socket)ar.AsyncState;

				// Complete the connection.  
				client.EndConnect(ar);

				Console.WriteLine("Socket connected to {0}",
					client.RemoteEndPoint.ToString());

				// Signal that the connection has been made.  
				connectDone.Set();
			}
			catch (Exception e)
			{
				//cant connect
				Console.WriteLine(e.ToString());
				Environment.Exit(0);
			}
		}



		private void Send(CSMessageWrapper wrapper)
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
				// Retrieve the socket from the state object.  
				Socket client = (Socket)ar.AsyncState;

				// Complete sending the data to the remote device.  
				int bytesSent = client.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to server.", bytesSent);

				// Signal that all bytes have been sent.  
				sendDone.Set();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}
		public void login(string username,int password)
		{
			CSMessageWrapper wrapper = new CSMessageWrapper();
			Login login = new Login();
			login.Name = username;
			login.Password = password;
			wrapper.Login = login;

			Send(wrapper);
		}

		public bool register(string username, string password1, string password2)
		{
			if (password1.Equals(password2))
			{
				CSMessageWrapper wrapper = new CSMessageWrapper();
				Register registerObj = new Register();
				registerObj.Username = username;
				registerObj.Password1 = password1.GetHashCode();

				wrapper.Register = registerObj;
				Send(wrapper);
				return true;
			}
			else
			{
				return false;
			}
		}

		public void joinLobby(string title)
		{
			//Send(Resources.c_joinLobby + title);
		}

		public void sendMessage(string message)
		{
			//Send(Resources.c_sendMessage +message);
		}

		public void makeLobby(string title)
		{
			CSMessageWrapper wrapper = new CSMessageWrapper();
			MakeRoom message = new MakeRoom();
			wrapper.MakeRoom = message;

			message.Title = title;

			Send(wrapper);
		}
	}
}