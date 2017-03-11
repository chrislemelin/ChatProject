using System;
using ProtoBuf;
namespace ChatServer
{
	[ProtoContract]
	public class LoginMessage
	{
		[ProtoMember(1)]
		public String username;

		public LoginMessage()
		{
		}
	}
}
