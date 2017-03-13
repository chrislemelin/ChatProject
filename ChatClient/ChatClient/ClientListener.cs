using System;
namespace ChatClient
{
	public interface ClientListener
	{
		void login(String Username,int password);

		void joinLobby(String title);

		void sendMessage(String message);

		void makeLobby(String title);
	}

}
