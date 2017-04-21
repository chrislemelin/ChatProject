using System;
using Gtk;
namespace ChatClient
{
	public partial class LobbyWindow : Gtk.Window
	{
		Gdk.Color backCol = new Gdk.Color();
		Gdk.Color focusCol = new Gdk.Color();
		public Reader rd;
		private EventBox lastClicked = null;
		private ModelClone modelClone;
		private Map<EventBox, RoomClone> rooms = new Map<EventBox, RoomClone>();
		private ServerProxy proxy;

		private MakeRoomWindow makeRoomWindow = null;
		private RoomWindow roomWindow = null;


		public LobbyWindow(ModelClone modelClone, ServerProxy proxy, Reader rd) :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			this.rd = rd;
			this.modelClone = modelClone;
			this.modelClone.lobbyWindow = this;
			this.proxy = proxy;

		}

		public void Start()
		{
			Gdk.Color.Parse("light grey", ref backCol);
			Gdk.Color.Parse("light blue", ref focusCol);
			eventbox2.ModifyBg(StateType.Normal, backCol);
			populateWindow();
			makeRoomButton.Pressed += OpenMakeRoomWindow;

			joinRoomButton.Sensitive = false;

			this.Show();
		}

		public EventBox addLabel(String s)
		{
			EventBox boxy = new EventBox();
			Label labie = new Label();

			labie.Text = s;
			boxy.Add(labie);
			labie.HeightRequest = 30;

			labie.Show();
			boxy.Show();
			boxy.ModifyBg(StateType.Normal, backCol);
			boxy.ButtonPressEvent += ButtonPressHandler;

			vbox4.PackStart(boxy, false, true, 0);
			return boxy;

		}

		private void populateWindow()
		{
			foreach (RoomClone room in modelClone.rooms)
			{
				rooms.Add(addLabel(room.Title),room);
			}
		}

		private void ButtonPressHandler(object obj, ButtonPressEventArgs args)
		{
			joinRoomButton.Sensitive = true;
			EventBox newEvent = (EventBox)obj;
			if (lastClicked != null)
			{
				lastClicked.ModifyBg(StateType.Normal, backCol);
			}
			lastClicked = newEvent;
			newEvent.ModifyBg(StateType.Normal, focusCol);
			Console.WriteLine( ((Label)newEvent.Children[0]).Text);
		}

		protected void OpenMakeRoomWindow(object sender, EventArgs e)
		{
			if (makeRoomWindow == null)
			{
				
				makeRoomWindow = new MakeRoomWindow(proxy);
				rd.makeRoomWindow = makeRoomWindow;
			}

		}

		protected void OnDeleteEvent(object o, DeleteEventArgs args)
		{
			//hides the register window instead of deleteing it
			//can only make one register window
			args.RetVal = true;
			Hide();
		}

		protected void OnJoinRoomButtonClicked(object sender, EventArgs e)
		{
			this.roomWindow = new RoomWindow(rooms.Forward[lastClicked], proxy);

			proxy.subRoom(rooms.Forward[lastClicked].ID, true);
			rd.roomWindow = roomWindow;
		}


	}
}
