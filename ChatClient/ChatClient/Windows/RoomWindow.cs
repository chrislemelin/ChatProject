using System;
using Gtk;
namespace ChatClient
{
	public partial class RoomWindow : Gtk.Window
	{
		private RoomClone room;
		private ServerProxy proxy;

		public RoomWindow(RoomClone room, ServerProxy proxy) :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			this.room = room;
			this.proxy = proxy;

			this.DestroyEvent += new DestroyEventHandler(OnDestroy);

				init();
		}

		private void init()
		{
			//titleLabel.Text = room.Title;
			Title = room.Title;
		}
    

		private void OnDestroy(object o, DestroyEventArgs args)
		{
			proxy.subRoom(room.ID, false);
		}



		public void addMessage(MessageClone ms)
		{
			Label lab = new Label();
			lab.Justify = Justification.Left;
			lab.SetAlignment(0, 0);
			lab.Text = ms.ToString();
			lab.Show();

			vboxMessages.PackStart(lab, true, true, 0);
			Adjustment adj = new Adjustment(0, 0, 0, 0, 0, 0);
			vboxMessages.SetScrollAdjustments(adj, adj);

		


		}

		protected void OnSendMessageButtonClicked(object sender, EventArgs e)
		{
			String s = sendMessageText.Buffer.Text;
			proxy.sendMessage(room.ID,s);
		}
	}
}
