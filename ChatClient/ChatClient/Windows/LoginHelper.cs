using System;
using System.Threading.Tasks;
using System.Threading;

namespace ChatClient
{
	public class LoginHelper
	{
		public LoginHelper()
		{
		}

		public RegisterWindow OpenRegisterWindow(RegisterWindow registerWindow, Reader rd, ServerProxy proxy) 
		{
			if (registerWindow == null)
			{
				ManualResetEvent wait = new ManualResetEvent(false);
				Gtk.Application.Invoke(delegate
				{
					registerWindow = new RegisterWindow();

					registerWindow.Show();
					rd.registerWindow = registerWindow;
					registerWindow.proxy = proxy;
					wait.Set();
				});
				wait.WaitOne();

			}

			Gtk.Application.Invoke(delegate
			{
				if (!registerWindow.Visible)
				{
					registerWindow.Show();
				}
			});
			return registerWindow;
		}

		public LobbyWindow StartLobbyWindow()
		{
			ManualResetEvent wait = new ManualResetEvent(false);
			LobbyWindow win= null;
			Gtk.Application.Invoke(delegate
			{
				win = new LobbyWindow();
				win.Start();
				wait.Set();
			});
			return win;
		}

	}
}
