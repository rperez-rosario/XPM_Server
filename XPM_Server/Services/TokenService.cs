using Microsoft.EntityFrameworkCore;
using XPM.Server.Crypto;
using XPM.Server.Interfaces;
using XPM.Server.Requests;
using XPM.Server.Responses;
using XPM_Server.ORM;

namespace XPM.Server.Services
{
  public class TokenService : ITokenService
  {
    private readonly XpmContext xpmDbContext;
    
    public TokenService(XpmContext XpmDbContext)
    {
      this.xpmDbContext = XpmDbContext;
    }
    
    public async Task<Tuple<string, string>> GenerateTokensAsync(int userId)
    {
      var accessToken = await Token.GenerateAccessToken(userId);
      var refreshToken = await Token.GenerateRefreshToken();
      var userRecord = await xpmDbContext.AuthUsers.Include(
        o => o.AuthRefreshTokens).FirstOrDefaultAsync(e => e.Id == userId);
      if (userRecord == null)
      {
        return new Tuple<String, String>(String.Empty, String.Empty);
      }
      var salt = PasswordHash.GetSecureSalt();
      var refreshTokenHashed = PasswordHash.HashUsingPbkdf2(refreshToken, salt);
      if (userRecord.AuthRefreshTokens != null && userRecord.AuthRefreshTokens.Any())
      {
        await RemoveRefreshTokenAsync(userRecord);
      }
      userRecord.AuthRefreshTokens?.Add(new AuthRefreshToken
      {
        ExpiryDate = DateTime.Now.ToUniversalTime().AddDays(30),
        TimeStamp = DateTime.Now.ToUniversalTime(),
        User = userId,
        TokenHash = refreshTokenHashed,
        TokenSalt = Convert.ToBase64String(salt)
      });
      await xpmDbContext.SaveChangesAsync();
      var token = new Tuple<string, string>(accessToken, refreshToken);
      return token;
    }
    public async Task<bool> RemoveRefreshTokenAsync(AuthUser user)
    {
      var userRecord = await xpmDbContext.AuthUsers.Include(
        o => o.AuthRefreshTokens).FirstOrDefaultAsync(e => e.Id == user.Id);
      if (userRecord == null)
      {
        return false;
      }
      if (userRecord.AuthRefreshTokens != null && userRecord.AuthRefreshTokens.Any())
      {
        var currentRefreshToken = userRecord.AuthRefreshTokens.First();
        xpmDbContext.AuthRefreshTokens.Remove(currentRefreshToken);
      }
      return false;
    }
    public async Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(
      RefreshTokenRequest refreshTokenRequest)
    {
      var refreshToken = await xpmDbContext.AuthRefreshTokens.FirstOrDefaultAsync(
        o => o.User == refreshTokenRequest.UserId);
      var response = new ValidateRefreshTokenResponse();
      if (refreshToken == null)
      {
        response.Success = false;
        response.Error = "Invalid session or user is already logged out";
        response.ErrorCode = "R02";
        return response;
      }
      var refreshTokenToValidateHash = PasswordHash.HashUsingPbkdf2(
        refreshTokenRequest.RefreshToken, Convert.FromBase64String(refreshToken.TokenSalt));
      if (refreshToken.TokenHash != refreshTokenToValidateHash)
      {
        response.Success = false;
        response.Error = "Invalid refresh token";
        response.ErrorCode = "R03";
        return response;
      }

      if (refreshToken.ExpiryDate < DateTime.UtcNow)
      {
        response.Success = false;
        response.Error = "Refresh token has expired";
        response.ErrorCode = "R04";
        return response;
      }
      response.Success = true;
      response.UserId = refreshToken.User;
      return response;
    }
  }
}