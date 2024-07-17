using System.ComponentModel.DataAnnotations;

namespace XPM.Server.Requests
{
  public class SignupRequest
  {
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public required string ConfirmPassword { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }
  }
}
