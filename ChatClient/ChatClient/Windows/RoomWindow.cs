using System;
namespace ChatClient
{
	public partial class RoomWindow : Gtk.Window
	{
		public RoomWindow() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
