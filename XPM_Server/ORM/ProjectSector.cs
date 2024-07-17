using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ProjectSector
{
    public int Id { get; set; }

    public string? Named { get; set; }

    public virtual ICollection<ProjectType> ProjectTypes { get; set; } = new List<ProjectType>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
