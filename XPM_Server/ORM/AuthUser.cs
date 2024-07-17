using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class AuthUser
{
    public int Id { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public bool Active { get; set; }

    public string Password { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public virtual ICollection<AuthRefreshToken> AuthRefreshTokens { get; set; } = new List<AuthRefreshToken>();

    public virtual ICollection<AuthUserEmailWhitelist> AuthUserEmailWhitelists { get; set; } = new List<AuthUserEmailWhitelist>();

    public virtual ICollection<AuthUserRole> AuthUserRoleAssignedByNavigations { get; set; } = new List<AuthUserRole>();

    public virtual ICollection<AuthUserRole> AuthUserRoleUserNavigations { get; set; } = new List<AuthUserRole>();

    public virtual ICollection<ChangeOrder> ChangeOrderCreatedByNavigations { get; set; } = new List<ChangeOrder>();

    public virtual ICollection<ChangeOrder> ChangeOrderLastUpdatedByNavigations { get; set; } = new List<ChangeOrder>();

    public virtual ICollection<Project> ProjectCreatedByNavigations { get; set; } = new List<Project>();

    public virtual ICollection<Project> ProjectLastUpdatedByNavigations { get; set; } = new List<Project>();
}
