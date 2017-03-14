using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace ChatClient
{
	public class RegistrationHelper
	{
		public RegistrationHelper()
		{
		}

		//public bool passwordsMatch()
		//{
		//}

		public bool passwordsStrongEnough(string password)
		{
			
			string regex = @"^ (?=.*[a - z])(?=.*[A - Z])(?=.*\d).{ 8,15}";
			Match match = Regex.Match(password, regex, RegexOptions.IgnoreCase);

			return match.Success;

		}
	}
}
