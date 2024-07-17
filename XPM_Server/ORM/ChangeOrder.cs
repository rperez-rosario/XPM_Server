using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

public partial class ChangeOrder
{
    public long Id { get; set; }

    public string? Number { get; set; }

    public int? Type { get; set; }

    public string? Description { get; set; }

    public int? Project { get; set; }

    public int? Status { get; set; }

    public decimal? Amount { get; set; }

    public DateOnly? NewProjectEndDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedOn { get; set; }

    public virtual AuthUser? CreatedByNavigation { get; set; }

    public virtual AuthUser? LastUpdatedByNavigation { get; set; }

    public virtual Project? ProjectNavigation { get; set; }

    public virtual ChangeOrderStatus? StatusNavigation { get; set; }

    public virtual ChangeOrderType? TypeNavigation { get; set; }
}
