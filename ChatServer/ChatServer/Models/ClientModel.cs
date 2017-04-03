using System;
namespace ChatServer
{
	public class ClientModel
	{
		public String username { get;}
		public int id { get;}
		public ClientProxy proxy { get; }

		public ClientModel(String username, int id,ClientProxy proxy)
		{
			this.username = username;
			this.id = id;
			this.proxy = proxy;
		}
	}
}
