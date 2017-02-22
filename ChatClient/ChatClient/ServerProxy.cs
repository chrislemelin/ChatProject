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
		public static readonly char EOM = (char)10;
		public static readonly char EOD = (char)11;
		public Socket client;
		private StringBuilder sb = new StringBuilder();
		private String response = String.Empty;
		public MessageInterperter interpreter = new MessageInterperter();


		public void Start()
		{
			Thread.CurrentThread.IsBackground = true;
			while (true)
			{
				Receive(client);
			}
		}

		private void Receive(Socket client)
		{
			try
			{
				byte[] bytes = new byte[256];

				int bytesRead = client.Receive(bytes);

				sb.Append(Encoding.ASCII.GetString(bytes, 0, bytesRead));
				response = sb.ToString();

				while(bytes[bytesRead-1] != (byte)EOM)
				{
					// There might be more data, so store the data received so far.  
					bytesRead = client.Receive(bytes);
					sb.Append(Encoding.ASCII.GetString(bytes, 0, bytesRead));
					response = sb.ToString();
				}
				response = sb.ToString();
				Console.WriteLine("--"+response+ "--");
				//interpreter.interpret(response);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

	}

	public class ServerProxy
	{
		// The port number for the remote device.  
		private const int port = 10000;

		// ManualResetEvent instances signal completion.  
		private ManualResetEvent connectDone =
			new ManualResetEvent(false);

		private ManualResetEvent sendDone =
			new ManualResetEvent(false);

		private Socket client;
  

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

				//Send(client, "This is a test<EOF>");
				//sendDone.WaitOne();

				Reader rd = new Reader();
				rd.client = client;
				Thread oThread = new Thread(new ThreadStart(rd.Start));
				oThread.Start();

				// Write the response to the console.  


				// Release the socket.  
				//client.Shutdown(SocketShutdown.Both);
				//client.Close();

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public void tryLogin(string username)
		{
			try
			{
				Send(this.client, username);
			}
			catch (Exception e)
			{
				
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
				Console.WriteLine(e.ToString());
			}
		}



		private void Send(Socket client, String data)
		{
			// Convert the string data to byte data using ASCII encoding.  
			byte[] byteData = Encoding.ASCII.GetBytes(data+ Reader.EOM);



			// Begin sending the data to the remote device.  
			client.BeginSend(byteData, 0, byteData.Length, 0,
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


	}
}