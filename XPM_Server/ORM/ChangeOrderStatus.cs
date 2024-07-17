using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ChangeOrderStatus
{
    public int Id { get; set; }

    public string? Named { get; set; }

    public virtual ICollection<ChangeOrder> ChangeOrders { get; set; } = new List<ChangeOrder>();
}
