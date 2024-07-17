using System.ComponentModel.DataAnnotations;

namespace XPM.Server.Requests
{
  public class ProjectDataRequest
  {
    [Required]
    public required int ProjectId { get; set; }
  }
}
