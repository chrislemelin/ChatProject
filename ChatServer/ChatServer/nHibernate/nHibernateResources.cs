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
			/*
			var mapper = new ModelMapper();
			mapper.Class<Room>(ca =>
			{
				ca.Id(x => x.ID, map =>
				{
					map.Column("ID");
					map.Generator(Generators.HighLow, gmap => gmap.Params(new { max_low = 100 }));
				});
				ca.Property(x => x.Title, map => map.Length(150));
				ca.Property(x => x.Owner);
			});
			*/


			var cfg = new NHibernate.Cfg.Configuration();
			var map = mapper.CompileMappingFor(Assembly.GetExecutingAssembly().GetExportedTypes());
			cfg.AddMapping(map);

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
