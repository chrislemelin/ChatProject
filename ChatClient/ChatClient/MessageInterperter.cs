using System;
using System.Text;
using System.Collections.Generic;

namespace ChatClient
{
	public class MessageInterperter
	{
		public MessageInterperter()
		{
		}

		private void interpret(String data)
		{
			String[] messages = data.Split(Resources.EOM);
			foreach (String s in messages)
			{
				interpretHelper(s.Split(Resources.EOD));
			}
		}

		private void interpretHelper(String[] data)
		{
			char type = data[0][0];
			StringBuilder sb = new StringBuilder(data[0]);
			sb.Remove(0, 1);
			switch (type)
			{
				case 'a':
					//init lobby
					sb.ToString();

					break;
			   	default:
            		Console.WriteLine("Default case");
					break;

			}


		}
	}
}
