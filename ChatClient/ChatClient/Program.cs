using System;
using Gtk;

namespace ChatClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			/*
			Application.Init();

			LobbyWindow win = new LobbyWindow();
			win.Start();
			for (int a = 0; a < 10; a++)
			{
				win.addLabel("test1");
				win.addLabel("test2");
				win.addLabel("test3");
				win.addLabel("test3");
			}
			Application.Run();
			*/

			ModelClone modelClone = new ModelClone();
			ServerProxy proxy = new ServerProxy(modelClone);
			Application.Init();
			LoginWindow win = new LoginWindow(modelClone,proxy);
		
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

		}
	}
}
