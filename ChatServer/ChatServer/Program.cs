using System;

namespace ChatServer
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			ConnectionListener listener = new ConnectionListener();
			Model model = new Model();
			listener.model = model;
			listener.StartListening();
		}
	}
}
