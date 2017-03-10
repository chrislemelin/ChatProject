using System;
using Gtk;
using ChatClient;

public partial class MainWindow : Gtk.Window
{
	public ServerProxy proxy = new ServerProxy();
	public MainWindow() : base(Gtk.WindowType.Toplevel)
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
		proxy.tryLogin(loginInput.Text);
	}

	protected void KeyPress(object o, KeyPressEventArgs args)
	{
	}
}
