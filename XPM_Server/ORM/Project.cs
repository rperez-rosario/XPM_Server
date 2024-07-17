using System;
using System.Collections.Generic;

namespace XPM_Server.ORM;

/// <summary>
/// Main project table.
/// </summary>
public partial class Project
{
    public int Id { get; set; }

    public string? Number { get; set; }

    public int? ContractType { get; set; }

    public int? ClientOwner { get; set; }

    public string? Named { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? Zip { get; set; }

    public int? ProjectSector { get; set; }

    public int? ProjectType { get; set; }

    public int? ProjectSubType { get; set; }

    public int? ProjectScope { get; set; }

    public int? ProjectDeliveryMethod { get; set; }

    public string? Notes { get; set; }

    public int? Contact { get; set; }

    public int? ProjectPrincipal { get; set; }

    public int? ProjectManager { get; set; }

    public int? ProjectArchitect { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateOnly? ConstructionStartDate { get; set; }

    public DateOnly? ConstructionScheduledCompletionDate { get; set; }

    public DateOnly? ConstructionEndDate { get; set; }

    public int? ConstructionCompany { get; set; }

    public decimal? InitialCost { get; set; }

    public decimal? FinalCost { get; set; }

    public decimal? ServiceFee { get; set; }

    public decimal? FinalFeesPaid { get; set; }

    public short? State { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedOn { get; set; }

    public virtual ICollection<AssignedDesigner> AssignedDesigners { get; set; } = new List<AssignedDesigner>();

    public virtual ICollection<AssignedInteriorDesigner> AssignedInteriorDesigners { get; set; } = new List<AssignedInteriorDesigner>();

    public virtual ICollection<ChangeOrder> ChangeOrders { get; set; } = new List<ChangeOrder>();

    public virtual ClientOwner? ClientOwnerNavigation { get; set; }

    public virtual ConstructionCompany? ConstructionCompanyNavigation { get; set; }

    public virtual ICollection<ConsultantEmployed> ConsultantEmployeds { get; set; } = new List<ConsultantEmployed>();

    public virtual Contact? ContactNavigation { get; set; }

    public virtual ContractType? ContractTypeNavigation { get; set; }

    public virtual AuthUser? CreatedByNavigation { get; set; }

    public virtual AuthUser? LastUpdatedByNavigation { get; set; }

    public virtual ProjectArchitect? ProjectArchitectNavigation { get; set; }

    public virtual ProjectDeliveryMethod? ProjectDeliveryMethodNavigation { get; set; }

    public virtual ProjectManager? ProjectManagerNavigation { get; set; }

    public virtual ProjectPrincipal? ProjectPrincipalNavigation { get; set; }

    public virtual ProjectScope? ProjectScopeNavigation { get; set; }

    public virtual ProjectSector? ProjectSectorNavigation { get; set; }

    public virtual ProjectSubType? ProjectSubTypeNavigation { get; set; }

    public virtual ProjectType? ProjectTypeNavigation { get; set; }

    public virtual ICollection<ServiceProvided> ServiceProvideds { get; set; } = new List<ServiceProvided>();

    public virtual State? StateNavigation { get; set; }
}
