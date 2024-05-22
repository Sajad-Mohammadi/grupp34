using karriarportalen.Server.Interfaces;
using karriarportalen.Server.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace karriarportalen.Server.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
		private readonly SymmetricSecurityKey _key;
		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
		}

		public string CreateToken(AppUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
				new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
			};

			var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds,
				Issuer = _configuration["JWT:Issuer"],
				Audience = _configuration["JWT:Audience"]
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}