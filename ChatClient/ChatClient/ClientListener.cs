using System;
namespace ChatClient
{
	public interface ClientListener
	{

		void login(String Username, int password);

		void joinLobby(String title);

		void sendMessage(int id, string message);

		void makeLobby(String title);

		void subRoom(int id, bool sub);
	}

}
