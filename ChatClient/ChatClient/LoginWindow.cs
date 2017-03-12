using System;
using Gtk;
using ChatClient;

public partial class LoginWindow : Gtk.Window
{
	public ServerProxy proxy = new ServerProxy();
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
		proxy.assignUsername(loginIn.Text);
	}

	public void DisplayMessage(String message)
	{
		display.Text = message;
	}

	protected void KeyPress(object o, KeyPressEventArgs args)
	{
	}
}
