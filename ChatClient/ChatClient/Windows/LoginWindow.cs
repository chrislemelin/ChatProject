using System;
using Gtk;
using ChatClient;
using System.Threading.Tasks;

public partial class LoginWindow : Gtk.Window
{
	public ServerProxy proxy = new ServerProxy();
	public Reader rd = new Reader();
	private RegisterWindow registerWindow;
	private LoginHelper loginHelper = new LoginHelper();
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
		display.Text = message;
	}

	protected void KeyPress(object o, KeyPressEventArgs args)
	{
	}

	protected async void OnRegisterButtonClicked(object sender, EventArgs e)
	{
		registerWindow = await Task.Run(() => loginHelper.OpenRegisterWindow(registerWindow, rd, proxy));
	}
}
