using System.ComponentModel.DataAnnotations;

namespace XPM.Server.Requests
{
  public class ContactPersonDataRequest
  {
    [Required]
    public required int ContactPersonId { get; set; }
  }
}
