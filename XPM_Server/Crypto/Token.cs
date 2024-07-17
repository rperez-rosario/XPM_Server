using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace XPM.Server.Crypto
{
  public class Token
  {
    public const string Issuer = "https://localhost:7285";
    public const string Audience = "https://localhost:7285";
    public const string Secret = "p0GXO6VuVZLRPef0tyO9jCqK4uZufDa6LP4n8Gj+8hQPB30f94pFiECAnPeMi5N6VT3/uscoGH7+zJrv4AuuPg==";
    public static async Task<string> GenerateAccessToken(int userId)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Convert.FromBase64String(Secret);
      var claimsIdentity = new ClaimsIdentity(new[] {
        new Claim(ClaimTypes.NameIdentifier, userId.ToString())
      });
      var signingCredentials = new SigningCredentials(
        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = claimsIdentity,
        Issuer = Issuer,
        Audience = Audience,
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = signingCredentials,
      };
      var securityToken = tokenHandler.CreateToken(tokenDescriptor);
      return await Task.Run(() => tokenHandler.WriteToken(securityToken));
    }
    public static async Task<string> GenerateRefreshToken()
    {
      var secureRandomBytes = new byte[32];
      using var randomNumberGenerator = RandomNumberGenerator.Create();
      await Task.Run(() => randomNumberGenerator.GetBytes(secureRandomBytes));
      var refreshToken = Convert.ToBase64String(secureRandomBytes);
      return refreshToken;
    }
  }
}