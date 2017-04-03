using System;
using Gtk;

namespace ChatClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
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
