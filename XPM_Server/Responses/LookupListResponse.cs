namespace XPM.Server.Responses
{
  public class LookupItem
  {
    public int Id { get; set; }
    public String? Named { get; set; }
  }

  public class LookupListResponse : BaseResponse
  {
    public List<LookupItem>? Item { get; set; }
  }
}
