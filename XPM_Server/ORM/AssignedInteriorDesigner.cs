using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class AssignedInteriorDesigner
{
    public long Id { get; set; }

    public int? Project { get; set; }

    public int? InteriorDesigner { get; set; }

    public virtual ProjectInteriorDesigner? InteriorDesignerNavigation { get; set; }

    public virtual Project? ProjectNavigation { get; set; }
}
