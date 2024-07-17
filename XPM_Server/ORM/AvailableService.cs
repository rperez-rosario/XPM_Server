using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class AvailableService
{
    public int Id { get; set; }

    public string? Named { get; set; }

    public virtual ICollection<ServiceProvided> ServiceProvideds { get; set; } = new List<ServiceProvided>();
}
