using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class AuthUserRole
{
    public int Id { get; set; }

    public int User { get; set; }

    public short Role { get; set; }

    public int AssignedBy { get; set; }

    public DateTime? AssignedOn { get; set; }

    public virtual AuthUser AssignedByNavigation { get; set; } = null!;

    public virtual AuthRole RoleNavigation { get; set; } = null!;

    public virtual AuthUser UserNavigation { get; set; } = null!;
}
