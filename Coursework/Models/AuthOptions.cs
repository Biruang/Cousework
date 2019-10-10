using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Models
{
	public class AuthOptions
	{
		public const string ISSUER = "ISSUER";
		public const string AUDIENCE = "AUDIENCE";
		public const string KEY = "System.ArgumentOutOfRangeException: IDX10603";
		public const int LIFETIME = 1;
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
		}
	}
}
