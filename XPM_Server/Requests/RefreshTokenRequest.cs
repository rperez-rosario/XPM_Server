using System.ComponentModel.DataAnnotations;

namespace XPM.Server.Requests
{
  public class RefreshTokenRequest
  {
    [Required]
    public int UserId { get; set; }
    [Required]
    public required string RefreshToken { get; set; }
  }
}
