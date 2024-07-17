using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ProjectDesigner
{
    public int Id { get; set; }

    public string? Named { get; set; }

    public virtual ICollection<AssignedDesigner> AssignedDesigners { get; set; } = new List<AssignedDesigner>();
}
