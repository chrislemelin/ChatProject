using System;
namespace ChatClient
{
	public class RoomClone
	{
		public string Title { get; set; }
		public int ID
		{
			get
			{
				return id;
			}
		}

		private int id;

		public RoomClone(int id)
		{
			this.id = id;
		}




	}
}
