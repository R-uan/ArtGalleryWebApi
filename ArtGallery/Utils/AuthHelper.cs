using ArtGallery.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArtGallery.Utils {
    public class AuthHelper {
        public static string GenerateJWTToken(Admin admin) {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, admin.Username.ToString()),
            new Claim(ClaimTypes.NameIdentifier, admin.Username.ToString()),
        };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-segredo-mega-super-grande-haha")),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}