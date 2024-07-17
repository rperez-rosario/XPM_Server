using XPM.Server.Requests;
using XPM.Server.Responses;

namespace XPM.Server.Interfaces
{
  public interface IProjectService
  {
    Task<ProjectListResponse> GetProjectListAsync();
    Task<LookupListResponse> GetContractTypeListAsync();
    Task<LookupListResponse> GetClientOwnerListAsync();
    Task<LookupListResponse> GetStateListAsync();
    Task<LookupListResponse> GetProjectScopeListAsync();
    Task<LookupListResponse> GetProjectSectorListAsync();
    Task<LookupListResponse> GetProjectTypeListAsync();
    Task<LookupListResponse> GetProjectSubTypeListAsync();
    Task<LookupListResponse> GetProjectDeliveryMethodListAsync();
    Task<LookupListResponse> GetContactListAsync();
    Task<LookupListResponse> GetConstructionCompanyListAsync();
    Task<LookupListResponse> GetProjectPrincipalListAsync();
    Task<LookupListResponse> GetProjectManagerListAsync();
    Task<LookupListResponse> GetProjectArchitectListAsync();
    
    Task<PostNewProjectResponse> PostNewProjectDataAsync(
      PostNewProjectRequest PostNewProjectRequest);
    Task<ProjectDataResponse> GetProjectDataAsync(
      ProjectDataRequest ProjectDataRequest);
    Task<UpdateProjectResponse> UpdateProjectDataAsync(
      UpdateProjectRequest UpdateProjectRequest);

    Task<ContactPersonDataResponse> GetContactPersonInformationAsync(
      ContactPersonDataRequest ContactPersonDataRequest);
    
    Task<LookupListResponse> GetSectorTypeAsync(
      SectorTypeRequest SectorTypeRequest);
    Task<LookupListResponse> GetTypeSubTypeAsync(
      TypeSubTypeRequest TypeSubTypeRequest);
  }
}
