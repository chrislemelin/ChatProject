using System;
using Gtk;

namespace ChatClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();

			LobbyWindow win = new LobbyWindow();
			win.Start();
			win.addLabel("test1");
			win.addLabel("test2");
			Application.Run();

			/*
			ServerProxy proxy = new ServerProxy();
			Application.Init();
			LoginWindow win = new LoginWindow();
			win.proxy = proxy;
			proxy.loginWindow = win;
			try
			{
				proxy.StartClient();
			}
			catch (Exception e)
			{
				win.Destroy();
				Application.Quit();
			}
			win.Show();
			Application.Run();
			*/
		}
	}
}
