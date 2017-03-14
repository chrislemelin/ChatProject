using System;

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
				Gtk.Application.Invoke(delegate
				{
					registerWindow = new RegisterWindow();
					registerWindow.Show();
					rd.registerWindow = registerWindow;
					registerWindow.proxy = proxy;
				});

			}
			return registerWindow;
		}

	}
}
