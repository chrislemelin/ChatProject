﻿using System;
using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
namespace ChatServer
{
	public class Room 
	{

		public virtual int ID { get; set; }

		public virtual string Title { get; set; }

		public virtual User Owner { get; set; }

		public Room()
		{


		}

	}
}