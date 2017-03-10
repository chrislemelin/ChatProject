﻿using System;
namespace ChatServer
{
	public class Resources
	{
		public static readonly char c_login = 'a';
		public static readonly char c_joinLobby = 'b';
		public static readonly char c_sendMessage = 'c';
		public static readonly char c_makeLobby = 'd';

		public static readonly char s_updateLobby = 'a';
		public static readonly char s_updateRoom = 'b';
		public static readonly char s_authenicated = 'c';

		public static readonly char EOM = (char)10;
		public static readonly char EOD = (char)11;

		public Resources()
		{
		}
	}
}
