using System.ComponentModel.DataAnnotations;

namespace XPM.Server.Requests
{
  public class SectorTypeRequest
  {
    [Required]
    public int SectorId { get; set; }
  }
}
