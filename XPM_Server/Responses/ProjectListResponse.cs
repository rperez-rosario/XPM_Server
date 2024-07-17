namespace XPM.Server.Responses
{
  public class ProjectListProject
  {
    public int Id { get; set; }
    public String? Number { get; set; }
    public String? Name { get; set; }
    public DateOnly? EndDate { get; set; }
    public String? LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedOn { get; set; }
  }

  public class ProjectListResponse : BaseResponse
  {
    public List<ProjectListProject>? Project { get; set; }
  }
}
