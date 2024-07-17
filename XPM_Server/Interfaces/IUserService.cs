using XPM.Server.Requests;
using XPM.Server.Responses;

namespace XPM.Server.Interfaces
{
  public interface IUserService
  {
    Task<TokenResponse> LoginAsync(LoginRequest LoginRequest);
    Task<SignupResponse> SignupAsync(SignupRequest SignupRequest);
    Task<LogoutResponse> LogoutAsync(int UserId);
  }
}
