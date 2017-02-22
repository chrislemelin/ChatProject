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
			String[] messages = data.Split(Reader.EOM);
			List<String[]> messageWhole = new List<String[]>();
			foreach (String s in messages)
			{
				interpretHelper(s.Split(Reader.EOD));
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
					break;
			   	default:
            		Console.WriteLine("Default case");
					break;

			}


		}
	}
}
