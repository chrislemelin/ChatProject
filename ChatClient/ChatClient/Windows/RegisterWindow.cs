using System;
namespace ChatClient
{
	public partial class RegisterWindow : Gtk.Window
	{
		public ServerProxy proxy = new ServerProxy();
		public RegisterWindow() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}

		protected void OnRegisterButtonClicked(object sender, EventArgs e)
		{
			proxy.register(UsernameIn.Text, PasswordIn1.Text, PasswordIn2.Text);
		}

		public void DisplayMessage(String s)
		{
			InfoDisplay.Text = s;

		}

	}
}
