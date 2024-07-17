using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace XPM.Server.Controllers
{
  public class BaseApiController : ControllerBase
  {
    protected int UserID;

    public BaseApiController()
    {
      GetUserID();
    }

    private void GetUserID()
    {
      string? claimNameIdentifier = FindClaim(ClaimTypes.NameIdentifier);
      if (claimNameIdentifier != null)
      {
        UserID = int.Parse(claimNameIdentifier);
      }
    }

    private string? FindClaim(string claimName)
    {
      ClaimsIdentity? claimsIdentity = null;
      Claim? claim = null;

      if (HttpContext != null && HttpContext.User != null && 
        HttpContext.User.Identity != null)
      {
        claimsIdentity = (ClaimsIdentity)HttpContext.User.Identity;
      }
      
      if (claimsIdentity != null)
      {
        claim = claimsIdentity.FindFirst(claimName);
      }
      
      if (claim == null)
      {
        return null;
      }

      return claim.Value;
    }
  }
}