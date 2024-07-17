using XPM.Server.Requests;
using XPM.Server.Responses;
using XPM_Server.ORM;

namespace XPM.Server.Interfaces
{
  public interface ITokenService
  {
    Task<Tuple<string, string>> GenerateTokensAsync(int UserId);
    Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(
      RefreshTokenRequest RefreshTokenRequest);
    Task<bool> RemoveRefreshTokenAsync(AuthUser AuthUser);
  }
}
