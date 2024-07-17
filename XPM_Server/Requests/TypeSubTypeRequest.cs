using System.ComponentModel.DataAnnotations;

namespace XPM.Server.Requests
{
  public class TypeSubTypeRequest
  {
    [Required]
    public int TypeId { get; set; }
  }
}
