using System;
using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using System.Collections.Generic;
using System.Reflection;

namespace ChatServer
{
	public class nHibernateResources
	{
		private static ISessionFactory factory;

		public static ISessionFactory Factory
		{
			get { return factory; }
		}

		public nHibernateResources()
		{
		}

		public static void init()
		{

			var cfg = new NHibernate.Cfg.Configuration();

			cfg.Configure();
			// ensure that mapping hbm.xml file is loaded
			cfg.AddAssembly(typeof(MainClass).Assembly);
			factory = cfg.BuildSessionFactory();

		}

		static public T Get<T, TValue>(string propertyName, TValue value, ISession session) where T : class
		{
			var par = System.Linq.Expressions.Expression.Parameter(typeof(T), "x");
			var eq = System.Linq.Expressions.Expression.Equal(System.Linq.Expressions.Expression.Property(par, propertyName), System.Linq.Expressions.Expression.Constant(value));
			var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(eq, par);
			return session.QueryOver<T>().Where(lambda).SingleOrDefault();
		}
	}
}
