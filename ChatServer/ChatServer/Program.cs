using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Tool.hbm2ddl;


namespace ChatServer
{
	class MainClass
	{
		public static void Main(string[] args)
		{

			nHibernateResources.init();
			Console.WriteLine("Hello World!");

			ConnectionListener listener = new ConnectionListener();
			Model model = new Model();
			listener.model = model;
			listener.StartListening();
		}

	}

}
