using System;
using Gtk;

namespace ChatClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			ServerProxy proxy = new ServerProxy();

			Application.Init();
			LoginWindow win = new LoginWindow();
			win.proxy = proxy;
			proxy.loginWindow = win;

			proxy.StartClient();
			win.Show();
			Application.Run();
		}
	}
}
