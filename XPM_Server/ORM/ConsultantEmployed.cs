using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ConsultantEmployed
{
    public long Id { get; set; }

    public int? Project { get; set; }

    public int? ConsultantDiscipline { get; set; }

    public string? ContactName { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public virtual ConsultantDiscipline? ConsultantDisciplineNavigation { get; set; }

    public virtual Project? ProjectNavigation { get; set; }
}
