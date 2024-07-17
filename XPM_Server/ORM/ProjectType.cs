using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ProjectType
{
    public int Id { get; set; }

    public string? Named { get; set; }

    public int? ProjectSector { get; set; }

    public virtual ProjectSector? ProjectSectorNavigation { get; set; }

    public virtual ICollection<ProjectSubType> ProjectSubTypes { get; set; } = new List<ProjectSubType>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
