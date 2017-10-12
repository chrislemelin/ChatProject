using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;

namespace ChatServer
{
	public class ConnectionListener
	{
		// Thread signal.  
		public ManualResetEvent allDone = new ManualResetEvent(false);
		public static readonly char EOM = (char)10;
		public Model model;

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
			Socket listener = new Socket(ipAddress.AddressFamily,
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
			State state = new State();
			state.workSocket = handler;

			Console.WriteLine("making thread");
			ClientProxy proxy = new ClientProxy(state.workSocket);
			model.addProxy(proxy);
			Reader rd = new Reader();
			rd.model = model;
			rd.proxy = proxy;
			rd.proxy = proxy;
			rd.socket = state.workSocket;
			Thread oThread = new Thread(new ThreadStart(rd.Start));
			oThread.Start();

		}
	}
}
