using System;

namespace ChatServer
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			ConnectionListener listener = new ConnectionListener();
			listener.StartListening();
		}
	}
}
