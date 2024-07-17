using System.ComponentModel.DataAnnotations;
using XPM.Server.Responses;

namespace XPM.Server.Requests
{
  public class UpdateProjectRequest
  {
    [Required]
    public int Id { get; set; }

    public String? Named { get; set; }
    public String? Number { get; set; }
    public int? ContractType { get; set; }
    public int? ClientOwner { get; set; }
    public String? AddressLine1 { get; set; }
    public String? AddressLine2 { get; set; }
    public String? City { get; set; }
    public String? Zip { get; set; }
    public short? State { get; set; }
    public int? ProjectSector { get; set; }
    public int? ProjectType { get; set; }
    public int? ProjectSubType { get; set; }
    public int? ProjectScope { get; set; }
    public int? ProjectDeliveryMethod { get; set; }
    public String? Notes { get; set; }
    public int? Contact { get; set; }
    public bool? ServiceDesignCriteriaProfessional { get; set; }
    public bool? ServiceMasterPlanning { get; set; }
    public bool? ServiceArchitecturalDesign { get; set; }
    public bool? ServiceProgramming { get; set; }
    public bool? ServiceInteriorDesign { get; set; }
    public bool? ServiceAsBuiltDocumentation { get; set; }
    public bool? ServiceSchematicDesign { get; set; }
    public bool? ServiceDesignDevelopment { get; set; }
    public bool? ServiceSiteDevelopment { get; set; }
    public bool? ServiceConstructionDocuments { get; set; }
    public bool? ServiceContractAdministration { get; set; }
    public bool? ServiceSitePlanApproval { get; set; }
    public bool? ServicePlatting { get; set; }
    public bool? ServiceBidding { get; set; }
    public bool? ServicePermitting { get; set; }
    public bool? ServiceConstructionAssistance { get; set; }
    public bool? ServiceEngineering { get; set; }
    public bool? ServiceCostEstimating { get; set; }
    public bool? ServiceLeedAdministration { get; set; }
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
    public decimal? OwnerRequestedChangeOrders { get; set; }
    public decimal? NonOwnerRequestedChangeOrders { get; set; }
    public decimal? ChangeOrdersTotalCost { get; set; }
    public decimal? ServiceFee { get; set; }
    public decimal? FinalFeesPaid { get; set; }
  }
}
