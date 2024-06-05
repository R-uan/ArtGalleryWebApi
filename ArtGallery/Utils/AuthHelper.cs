using ArtGallery.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArtGallery.Utils;

public class AuthHelper {
	public static string Generate(Admin admin) {
		try {
			var key = Encoding.UTF8.GetBytes("super-segredo-mega-super-grande-haha");
			var handler = new JwtSecurityTokenHandler();
			var credentials = new SigningCredentials(
				new SymmetricSecurityKey(key),
				SecurityAlgorithms.HmacSha256Signature
			);
			var tokenDescriptor = new SecurityTokenDescriptor {
				Subject = GenerateClaims(admin),
				Expires = DateTime.UtcNow.AddHours(2),
				SigningCredentials = credentials,
			};
			var token = handler.CreateToken(tokenDescriptor);
			return handler.WriteToken(token);
		} catch (System.Exception e) {
			return e.Message;
		}
	}

	private static ClaimsIdentity GenerateClaims(Admin user) {
		var ci = new ClaimsIdentity();
		ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
		return ci;
	}

}
