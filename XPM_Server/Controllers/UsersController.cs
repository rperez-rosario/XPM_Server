using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XPM.Server.Interfaces;
using XPM.Server.Requests;
using XPM.Server.Responses;

namespace XPM.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : BaseApiController
  {
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    
    public UsersController(IUserService userService, ITokenService tokenService)
    {
      this.userService = userService;
      this.tokenService = tokenService;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
      if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || 
        string.IsNullOrEmpty(loginRequest.Password))
      {
        return BadRequest(new TokenResponse
        {
          Error = "Missing login details",
          ErrorCode = "L01"
        });
      }
      var loginResponse = await userService.LoginAsync(loginRequest);
      if (!loginResponse.Success)
      {
        return Unauthorized(new
        {
          loginResponse.ErrorCode,
          loginResponse.Error
        });
      }
      return Ok(loginResponse);
    }
    
    [HttpPost]
    [Route("refresh_token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
      if (refreshTokenRequest == null || 
        string.IsNullOrEmpty(refreshTokenRequest.RefreshToken) || 
        refreshTokenRequest.UserId <= 0)
      {
        return BadRequest(new TokenResponse
        {
          Error = "Missing refresh token and or token details",
          ErrorCode = "R01"
        });
      }

      ValidateRefreshTokenResponse validateRefreshTokenResponse = 
        await tokenService.ValidateRefreshTokenAsync(refreshTokenRequest);
  
      if (!validateRefreshTokenResponse.Success)
      {
        return UnprocessableEntity(validateRefreshTokenResponse);
      }

      var tokenResponse = await tokenService.GenerateTokensAsync(
        validateRefreshTokenResponse.UserId);

      if (tokenResponse.Item1.Equals(String.Empty) || 
        tokenResponse.Item2.Equals(String.Empty))
      {
        return BadRequest(new TokenResponse
        {
          Error = "Missing token response details",
          ErrorCode = "R02"
        });
      }
      
      return Ok(
        new { AccessToken = tokenResponse.Item1, Refreshtoken = tokenResponse.Item2 });
    }
    
    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> Signup(SignupRequest signupRequest)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values.SelectMany(
          x => x.Errors.Select(c => c.ErrorMessage)).ToList();
        if (errors.Any())
        {
          return BadRequest(new TokenResponse
          {
            Error = $"{string.Join(",", errors)}",
            ErrorCode = "S01"
          });
        }
      }

      var signupResponse = await userService.SignupAsync(signupRequest);
      if (!signupResponse.Success)
      {
        return UnprocessableEntity(signupResponse);
      }
      return Ok(signupResponse.Email);
    }

    [Authorize]
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
      var logout = await userService.LogoutAsync(UserID);
      if (!logout.Success)
      {
        return UnprocessableEntity(logout);
      }
      return Ok();
    }
  }
}
