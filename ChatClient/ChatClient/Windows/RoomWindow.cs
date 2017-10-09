using System;
using Gtk;
using System.Threading;
namespace ChatClient
{
	public partial class RoomWindow : Gtk.Window
	{
		private RoomClone room;
		private ServerProxy proxy;
		private Thread tr;

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
			titleLabel.Text = room.Title;
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

			vboxMessages.PackStart(lab, false, true, 0);
			vboxMessages.ShowNow();
			/*
			Adjustment adj = scrolledMessageWindow.Vadjustment;
			if (tr != null)
				tr.Abort();

			Gtk.Application.Invoke(delegate
			{
				scrollToBottom(adj.Upper - adj.PageSize);
			});
			*/

		}

		protected void OnSendMessageButtonClicked(object sender, EventArgs e)
		{
			String s = sendMessageText.Buffer.Text;

			Thread messageThread = nechw Thread(() => proxy.sendMessage(room.ID, s));
			messageThread.Start();	
		}

		protected void OnVboxMessagesAdded(object o, AddedArgs args)
		{
			Adjustment adj = scrolledMessageWindow.Vadjustment;
			if (tr != null)
				tr.Abort();

			// unfortunatly dont see another way to do this
			// no call back when the 
			//Thread.Sleep(100);
			Thread messageThread = new Thread(() =>
			{
				//Thread.Sleep(100);
				Gtk.Application.Invoke(delegate
				{
					scrollToBottom(adj.Upper - adj.PageSize);
				});
			});
			messageThread.Start();
		}

		private void scrollToBottom(double current)
		{
			Adjustment adj = scrolledMessageWindow.Vadjustment;
			adj.Value = current;

		}
	}
}
