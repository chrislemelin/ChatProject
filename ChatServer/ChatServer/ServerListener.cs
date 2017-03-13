using System;
namespace ChatServer
{
	public interface ServerListener
	{
		void updateLobby(String update);

		void updateRoom(String update);

		void authenticated(bool success);

		void registerResponse(bool success);
	}
}
