using System;

using System.Threading.Tasks;
using Gtk;

namespace ChatClient
{
	public partial class RegisterWindow : Gtk.Window
	{

		public ServerProxy proxy = new ServerProxy();

		private RegistrationHelper registrationHelper;


		public RegisterWindow() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();

		}



		protected async void  OnRegisterButtonClicked(object sender, EventArgs e)
		{
			bool success = await Task.Run(() => registrationHelper.passwordsStrongEnough(PasswordIn1.Text));
			if (success)
			{
				proxy.register(UsernameIn.Text, PasswordIn1.Text, PasswordIn2.Text);
			}
			else
			{
				DisplayMessage("password not strong enough");
			}

		}

		public void DisplayMessage(String s)
		{
			InfoDisplay.Text = s;

		}

	}
}
