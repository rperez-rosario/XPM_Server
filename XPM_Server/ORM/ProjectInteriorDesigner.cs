using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ProjectInteriorDesigner
{
    public int Id { get; set; }

    public string? Named { get; set; }

    public virtual ICollection<AssignedInteriorDesigner> AssignedInteriorDesigners { get; set; } = new List<AssignedInteriorDesigner>();
}
