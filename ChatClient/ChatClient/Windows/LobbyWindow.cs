using System;
using Gtk;
namespace ChatClient
{
	public partial class LobbyWindow : Gtk.Window
	{
		Gdk.Color backCol = new Gdk.Color();
		Gdk.Color focusCol = new Gdk.Color();
		private EventBox lastClicked = null;
		private ModelClone modelClone;
		private Map<EventBox, RoomClone> rooms = new Map<EventBox, RoomClone>();


		public LobbyWindow(ModelClone modelClone) :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			this.modelClone = modelClone;
			this.modelClone.lobbyWindow = this;

		}

		public void Start()
		{
			Gdk.Color.Parse("light grey", ref backCol);
			Gdk.Color.Parse("light blue", ref focusCol);
			eventbox2.ModifyBg(StateType.Normal, backCol);
			populateWindow();
			this.Show();
		}

		public EventBox addLabel(String s)
		{
			EventBox boxy = new EventBox();
			Label labie = new Label();

			labie.Text = s;
			boxy.Add(labie);
			labie.HeightRequest =30;

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
			EventBox newEvent = (EventBox)obj;
			if (lastClicked != null)
			{
				lastClicked.ModifyBg(StateType.Normal, backCol);
			}
			lastClicked = newEvent;
			newEvent.ModifyBg(StateType.Normal, focusCol);
			Console.WriteLine( ((Label)newEvent.Children[0]).Text);
		}

	}
}
