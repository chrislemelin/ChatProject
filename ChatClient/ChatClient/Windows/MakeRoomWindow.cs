using System;
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
			this.proxy = proxy;
		}

		public void MakeRoom(Object o, EventArgs args)
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

	}
}
