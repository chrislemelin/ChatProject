using System;
namespace ChatClient
{
	public interface ClientListener
	{
		void assignUsername(String Username);

		void joinLobby(String title);

		void sendMessage(String message);

		void makeLobby(String title);
	}

}
