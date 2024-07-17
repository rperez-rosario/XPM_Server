using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class AuthUserEmailWhitelist
{
    public int Id { get; set; }

    public string EmailAddress { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;
}
