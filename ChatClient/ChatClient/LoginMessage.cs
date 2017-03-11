using System;
using ProtoBuf;
namespace ChatClient
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
