using System;
using Gtk;
using ChatClient;

public partial class LoginWindow : Gtk.Window
{
	public ServerProxy proxy = new ServerProxy();
	public Reader rd = new Reader();
	private RegisterWindow registerWindow;
	public LoginWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void Login(object sender, EventArgs e)
	{
		proxy.login(loginIn.Text,passwordIn.Text.GetHashCode());
	}

	public void DisplayMessage(String message)
	{
		//display.Text = message;
	}

	protected void KeyPress(object o, KeyPressEventArgs args)
	{
	}

	protected void OnRegisterButtonClicked(object sender, EventArgs e)
	{
		if (registerWindow == null)
		{
			registerWindow = new RegisterWindow();
			rd.registerWindow = registerWindow;
			//registerWindow.proxy = proxy;
			registerWindow.Show();
		}
	}
}
