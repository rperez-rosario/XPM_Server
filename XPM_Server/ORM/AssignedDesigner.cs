using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class AssignedDesigner
{
    public long Id { get; set; }

    public int? Project { get; set; }

    public int? ProjectDesigner { get; set; }

    public virtual ProjectDesigner? ProjectDesignerNavigation { get; set; }

    public virtual Project? ProjectNavigation { get; set; }
}
