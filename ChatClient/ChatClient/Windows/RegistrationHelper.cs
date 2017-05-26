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

	
		// returns display message if password invalid, else returns null
		public string passwordCheck(string password1, string password2)
		{
			if (password1.Equals(password2))
			{
				string regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}";
				Match match = Regex.Match(password1, regex, RegexOptions.None);
				if (match.Success)
				{
					return null;
				}
				else
				{
					return "password not strong enough, please include 1 lowercase,\n"+
						"1 uppercase, 1 digit, 1 special character and be greater than 7 characters";
				}
			}

			return "passwords much match";

		}
	}
}
