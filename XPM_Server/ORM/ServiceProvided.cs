using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ServiceProvided
{
    public long Id { get; set; }

    public int? Project { get; set; }

    public int? AvailableService { get; set; }

    public virtual AvailableService? AvailableServiceNavigation { get; set; }

    public virtual Project? ProjectNavigation { get; set; }
}
