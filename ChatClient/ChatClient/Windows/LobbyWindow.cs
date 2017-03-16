using System;
using Gtk;
namespace ChatClient
{
	public partial class LobbyWindow : Gtk.Window
	{
		Gdk.Color backCol = new Gdk.Color();
		Gdk.Color focusCol = new Gdk.Color();
		private EventBox lastClicked = null;

		public LobbyWindow() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}

		public void Start()
		{
			Gdk.Color.Parse("light grey", ref backCol);
			Gdk.Color.Parse("light blue", ref focusCol);
			eventbox2.ModifyBg(StateType.Normal, backCol);
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

			vbox4.PackStart(boxy, false, false, 0);
			return boxy;

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
