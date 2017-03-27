using System;
using Gtk;
using ChatClient;
using System.Threading.Tasks;

public partial class LoginWindow : Gtk.Window
{
	public ServerProxy proxy;
	public Reader rd;
	public ModelClone modelClone;

	private RegisterWindow registerWindow;
	private LoginHelper loginHelper = new LoginHelper();
	public LoginWindow(ModelClone modelClone, ServerProxy proxy) : base(Gtk.WindowType.Toplevel)
	{
		Build();
		this.modelClone = modelClone;
		this.proxy = proxy;
	}

	public void DisplayMessage(String message)
	{
		display.Text = message;
	}

	public async void StartLobbyWindow()
	{
		//await Task.Run(() => loginHelper.StartLobbyWindow(modelClone,proxy));
		Gtk.Application.Invoke(delegate
		{
			LobbyWindow win = new LobbyWindow(modelClone, proxy);
			win.Start();
		});
		if (registerWindow != null)
		{
			registerWindow.Destroy();
		}
		this.Destroy();
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


	protected void KeyPress(object o, KeyPressEventArgs args)
	{
	}

	protected void OnRegisterButtonClicked(object sender, EventArgs e)
	{
		if (registerWindow == null)
		{
			registerWindow = new RegisterWindow();

			registerWindow.Show();
			rd.registerWindow = registerWindow;
			registerWindow.proxy = proxy;
		}
		if (!registerWindow.Visible)
		{
			registerWindow.Show();
		}


	}
}
