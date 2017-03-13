using System;
namespace ChatServer
{
	public class Product
	{
		public virtual int PersonID { get; set; }

		public virtual string FirstName { get; set; }

		public virtual string LastName { get; set; }

		public Product()
		{

		}
	}
}
