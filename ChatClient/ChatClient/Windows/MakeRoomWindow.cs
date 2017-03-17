using System;
namespace ChatClient
{
	public partial class MakeRoomWindow : Gtk.Window
	{
		public MakeRoomWindow() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
