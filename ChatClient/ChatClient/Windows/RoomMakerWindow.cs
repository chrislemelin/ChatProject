using System;
using Gtk;

namespace ChatClient
{
	public partial class MakeRoomWindow : Gtk.Window
	{
		private ServerProxy proxy;
		public MakeRoomWindow(ServerProxy proxy) :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			MakeRoomButton.Pressed += MakeRoom;
			this.DeleteEvent += OnDeleteEvent;

			this.proxy = proxy;
		}

		public void MakeRoom(object sender, EventArgs e)
		{
			if (!RoomNameIn.Text.Equals(""))
			{
				proxy.makeLobby(RoomNameIn.Text);
			}
			else
			{
				DescriptionBox.Buffer.Text = "fill out room name";
			}
		}

		public void DisplayMessage(String text)
		{
			DescriptionBox.Buffer.Text = text;
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
