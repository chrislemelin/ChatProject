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
			Console.WriteLine("Hello World!");

			Product [] myInitialObjects = new Product[2];
			myInitialObjects[0] = new Product { LastName = "boby", FirstName = "danny" };
			myInitialObjects[1] = new Product { LastName = "dammy", FirstName = "kacjies" };


			var cfg = new NHibernate.Cfg.Configuration();
			cfg.Configure();
			// ensure that mapping hbm.xml file is loaded
			cfg.AddAssembly(typeof(MainClass).Assembly);
			ISessionFactory factory =
				cfg.BuildSessionFactory();

			using (ISession session = factory.OpenSession())
			{
				// Insert two employees in Database
				session.Save(myInitialObjects[0]);
				session.Save(myInitialObjects[1]);
				session.Flush();
				//session.Close();
			}

			Console.WriteLine("Hello World!");
		


			ConnectionListener listener = new ConnectionListener();
			Model model = new Model();
			listener.model = model;
			listener.StartListening();
		}
	}
}
