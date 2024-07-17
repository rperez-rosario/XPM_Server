using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ProjectSubType
{
    public int Id { get; set; }

    public string? Named { get; set; }

    public int? ProjectType { get; set; }

    public virtual ProjectType? ProjectTypeNavigation { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
