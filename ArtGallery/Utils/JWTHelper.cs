using ArtGallery.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArtGallery.Utils;

public class JWTHelper(IOptions<JWTSettings> jwtSettings)
{
	private readonly JWTSettings _jwtSettings = jwtSettings.Value;
	public string Generate(Admin admin)
	{
		try
		{
			var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
			var handler = new JwtSecurityTokenHandler();
			var credentials = new SigningCredentials(
				new SymmetricSecurityKey(key),
				SecurityAlgorithms.HmacSha256Signature
			);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = _jwtSettings.Issuer,
				Subject = GenerateClaims(admin),
				SigningCredentials = credentials,
				Expires = DateTime.UtcNow.AddHours(2),
			};
			var token = handler.CreateToken(tokenDescriptor);
			return handler.WriteToken(token);
		}
		catch (System.Exception e)
		{
			return e.Message;
		}
	}

	private static ClaimsIdentity GenerateClaims(Admin user)
	{
		var ci = new ClaimsIdentity();
		ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
		return ci;
	}
}
