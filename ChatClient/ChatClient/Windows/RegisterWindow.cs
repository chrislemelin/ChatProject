using System;

using System.Threading.Tasks;
using Gtk;

namespace ChatClient
{
	public partial class RegisterWindow : Gtk.Window
	{

		public ServerProxy proxy = new ServerProxy();
		private RegistrationHelper registrationHelper = new RegistrationHelper();

		public RegisterWindow() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();

		}

		protected async void  OnRegisterButtonClicked(object sender, EventArgs e)
		{
			string result = await Task.Run(() => registrationHelper.passwordCheck(PasswordIn1.Text,PasswordIn2.Text));
			if (result == null)
			{
				await Task.Run(() => proxy.register(UsernameIn.Text, PasswordIn1.Text, PasswordIn2.Text));
			}
			else
			{
				DisplayMessage(result);
			}
		}

		public void DisplayMessage(String s)
		{
			InfoDisplay.Text = s;

		}

		protected void OnDeleteEvent(object o, DeleteEventArgs args)
		{
			//hides the register window instead of deleteing it
			//can only make one register window
			args.RetVal = true;
			Hide();
		}
	}
}
