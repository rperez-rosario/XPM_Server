using Microsoft.EntityFrameworkCore;
using XPM.Server.Crypto;
using XPM.Server.Interfaces;
using XPM.Server.Requests;
using XPM.Server.Responses;
using XPM_Server.ORM;

namespace XPM.Server.Services
{
  public class UserService : IUserService
  {
    private readonly XpmContext xpmDbContext;
    private readonly ITokenService tokenService;

    public UserService(XpmContext XpmDbContext, ITokenService TokenService)
    {
      this.xpmDbContext = XpmDbContext;
      this.tokenService = TokenService;
    }
    public async Task<TokenResponse> LoginAsync(LoginRequest loginRequest)
    {
      var user = xpmDbContext.AuthUsers.SingleOrDefault(
        user => user.Active && user.EmailAddress == loginRequest.Email);
      if (user == null)
      {
        return new TokenResponse
        {
          Success = false,
          Error = "Email not found",
          ErrorCode = "L02"
        };
      }
      var passwordHash = PasswordHash.HashUsingPbkdf2(
        loginRequest.Password, Convert.FromBase64String(user.PasswordSalt));
      if (user.Password != passwordHash)
      {
        return new TokenResponse
        {
          Success = false,
          Error = "Invalid Password",
          ErrorCode = "L03"
        };
      }
      var token = await System.Threading.Tasks.Task.Run(() => 
        tokenService.GenerateTokensAsync(user.Id));
      return new TokenResponse
      {
        Success = true,
        AccessToken = token.Item1,
        RefreshToken = token.Item2
      };
    }
    public async Task<LogoutResponse> LogoutAsync(int userId)
    {
      var refreshToken = 
        await xpmDbContext.AuthRefreshTokens.FirstOrDefaultAsync(o => o.User == userId);
      if (refreshToken == null)
      {
        return new LogoutResponse { Success = true };
      }
      xpmDbContext.AuthRefreshTokens.Remove(refreshToken);
      var saveResponse = await xpmDbContext.SaveChangesAsync();
      if (saveResponse >= 0)
      {
        return new LogoutResponse { Success = true };
      }
      return new LogoutResponse { 
        Success = false, Error = "Unable to logout user", ErrorCode = "L04" };
    }
    public async Task<SignupResponse> SignupAsync(SignupRequest signupRequest)
    {
      var existingUser = await xpmDbContext.AuthUsers.SingleOrDefaultAsync(
        user => user.EmailAddress == signupRequest.Email);
      if (existingUser != null)
      {
        return new SignupResponse
        {
          Success = false,
          Error = "User already exists with the same email",
          ErrorCode = "S02"
        };
      }
      if (signupRequest.Password != signupRequest.ConfirmPassword)
      {
        return new SignupResponse
        {
          Success = false,
          Error = "Password and confirm password do not match",
          ErrorCode = "S03"
        };
      }
      // This can be more complicated than only length,
      // you can check on alphanumeric and or special characters
      if (signupRequest.Password.Length <= 7) 
      {
        return new SignupResponse
        {
          Success = false,
          Error = "Password is weak",
          ErrorCode = "S04"
        };
      }
      var salt = PasswordHash.GetSecureSalt();
      var passwordHash = PasswordHash.HashUsingPbkdf2(signupRequest.Password, salt);
      var user = new AuthUser
      {
        EmailAddress = signupRequest.Email,
        Password = passwordHash,
        PasswordSalt = Convert.ToBase64String(salt),
        FirstName = signupRequest.FirstName,
        LastName = signupRequest.LastName,
        CreatedOn = DateTime.UtcNow,
        Active = true // You can save is false and send confirmation email to the user,
                      // then once the user confirms the email you can make it true
      };
      await xpmDbContext.AuthUsers.AddAsync(user);
      var saveResponse = await xpmDbContext.SaveChangesAsync();
      if (saveResponse >= 0)
      {
        return new SignupResponse { Success = true, Email = user.EmailAddress };
      }
      return new SignupResponse
      {
        Success = false,
        Error = "Unable to save the user",
        ErrorCode = "S05"
      };
    }
  }
}