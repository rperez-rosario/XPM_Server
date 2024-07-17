using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class AuthRefreshToken
{
    public long Id { get; set; }

    public int User { get; set; }

    public string TokenHash { get; set; } = null!;

    public string TokenSalt { get; set; } = null!;

    public DateTime TimeStamp { get; set; }

    public DateTime ExpiryDate { get; set; }

    public virtual AuthUser UserNavigation { get; set; } = null!;
}
