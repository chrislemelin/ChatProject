using System;
using System.Collections.Generic;
namespace ChatServer
{
	public class Model
	{
		//private List<IRoom> rooms;
		//private Dictionary<ClientProxy, ClientModel> clients_proxy;
		private Dictionary<String, ClientModel> clients_name;
		int clientsIdCounter = 0;
		//private IRoom lobby;


		public Model()
		{
			clients_name = new Dictionary<string, ClientModel>();
		}

		public bool addUser(String username,ClientProxy proxy)
		{
			//makes sure there isn't already a user with the given username
			if (!clients_name.ContainsKey(username.Trim()))
			{
				
				ClientModel client = new ClientModel();
				client.name = username.Trim();
				client.id = clientsIdCounter++;
				clients_name.Add(username.Trim(), client);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
