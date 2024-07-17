using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class AuthRole
{
    public short Id { get; set; }

    public string Named { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<AuthUserRole> AuthUserRoles { get; set; } = new List<AuthUserRole>();
}
