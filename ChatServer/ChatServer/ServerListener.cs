using System;
using System.Collections.Generic;
namespace ChatServer
{
	public interface ServerListener
	{
		void updateLobby(List<UpdateLobbyPiece> pieces);

		void updateRoom(String update);

		void authenticated(bool success);

		void registerResponse(bool success);
	}
}
