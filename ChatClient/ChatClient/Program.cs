using System.Net.Sockets;
using System;
using Gtk;
using System.Threading;

namespace ChatClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			ModelClone modelClone = new ModelClone();
			ServerProxy proxy = new ServerProxy(modelClone);
			Application.Init();
			System.Net.Sockets.Socket client = null;
			try
			{
				client = proxy.StartClient();
			}
			catch (Exception e)
			{
				Application.Quit();
			}

			LoginWindow loginWindow = new LoginWindow(modelClone, proxy);
			Reader rd = new Reader(modelClone);
			rd.loginWindow = loginWindow;
			rd.client = client;
			loginWindow.rd = rd;
			Thread oThread = new Thread(new ThreadStart(rd.Start));
			oThread.Start();

			loginWindow.Show();
			Application.Run();

		}
	}
}
