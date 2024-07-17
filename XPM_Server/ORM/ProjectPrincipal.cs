using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ProjectPrincipal
{
    public int Id { get; set; }

    public string? Named { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
