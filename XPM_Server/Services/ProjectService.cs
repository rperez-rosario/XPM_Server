using Microsoft.EntityFrameworkCore;
using XPM.Server.Interfaces;
using XPM.Server.Responses;
using XPM_Server.ORM;
using XPM.Server.Requests;

namespace XPM.Server.Services
{
  public class ProjectService : IProjectService
  {
    private readonly XpmContext xpmDbContext;

    public ProjectService(XpmContext XpmDbContext)
    {
      this.xpmDbContext = XpmDbContext;
    }

    public async Task<PostNewProjectResponse> PostNewProjectDataAsync(
      PostNewProjectRequest PostNewProjectRequest)
    {
      Project project = new Project();
      PostNewProjectResponse postNewProjectResponse = new PostNewProjectResponse();

      project.Named = PostNewProjectRequest.Named;
      project.Number = PostNewProjectRequest.Number;
      project.ContractType = PostNewProjectRequest.ContractType;
      project.ClientOwner = PostNewProjectRequest.ClientOwner;
      project.AddressLine1 = PostNewProjectRequest.AddressLine1;
      project.AddressLine2 = PostNewProjectRequest.AddressLine2;
      project.City = PostNewProjectRequest.City;
      project.Zip = PostNewProjectRequest.Zip;
      project.State = PostNewProjectRequest.State;
      project.ProjectSector = PostNewProjectRequest.ProjectSector;
      project.ProjectType = PostNewProjectRequest.ProjectType;
      project.ProjectSubType = PostNewProjectRequest.ProjectSubType;
      project.ProjectScope = PostNewProjectRequest.ProjectScope;
      project.ProjectDeliveryMethod = PostNewProjectRequest.ProjectDeliveryMethod;
      project.Notes = PostNewProjectRequest.Notes;
      project.Contact = PostNewProjectRequest.Contact;
      project.ProjectPrincipal = PostNewProjectRequest.ProjectPrincipal;
      project.ProjectManager = PostNewProjectRequest.ProjectManager;
      project.ProjectArchitect = PostNewProjectRequest.ProjectArchitect;
      project.StartDate = PostNewProjectRequest.StartDate;
      project.EndDate = PostNewProjectRequest.EndDate;
      project.ConstructionStartDate = PostNewProjectRequest.ConstructionStartDate;
      project.ConstructionScheduledCompletionDate =
        PostNewProjectRequest.ConstructionScheduledCompletionDate;
      project.ConstructionEndDate = PostNewProjectRequest.ConstructionEndDate;
      project.ConstructionCompany = PostNewProjectRequest.ConstructionCompany;
      project.InitialCost = PostNewProjectRequest.InitialCost;
      project.FinalCost = PostNewProjectRequest.FinalCost;
      project.ServiceFee = PostNewProjectRequest.ServiceFee;
      project.FinalFeesPaid = PostNewProjectRequest.FinalFeesPaid;
      project.CreatedBy = 13;
      project.CreatedOn = DateTime.UtcNow;
      project.LastUpdatedBy = 13;
      project.LastUpdatedOn = DateTime.UtcNow;

      try
      {
        await xpmDbContext.AddAsync(project);
        await xpmDbContext.SaveChangesAsync();

        if (project.Id > 0)
        {
          if (await PostNewProjectServiceInformationAsync(
            project.Id, PostNewProjectRequest))
          {
            postNewProjectResponse.NewProjectId = project.Id;
            postNewProjectResponse.Success = true;
          }
          else
          {
            postNewProjectResponse.Error =
              "New project service insert did not complete successfully";
            postNewProjectResponse.ErrorCode = "P17";
            postNewProjectResponse.Success = false;
          }
        }
        else
        {
          postNewProjectResponse.Error =
            "New project insert did not return valid new id.";
          postNewProjectResponse.ErrorCode = "P16";
          postNewProjectResponse.Success = false;
        }
      }
      catch (Exception ex)
      {
        postNewProjectResponse.Error =
          "Could not post new project information to database. " + ex.Message;
        postNewProjectResponse.ErrorCode = "P15";
        postNewProjectResponse.Success = false;
      }
      return postNewProjectResponse;
    }

    public async Task<ProjectDataResponse> GetProjectDataAsync(
      ProjectDataRequest ProjectDataRequest)
    {
      Project project = new Project();
      ProjectDataResponse projectDataResponse = new ProjectDataResponse();
      List<ServiceProvided> serviceProvided = new List<ServiceProvided>();

      try
      {
        project = await xpmDbContext.Projects.Where(
          x => x.Id == ProjectDataRequest.ProjectId).SingleAsync();
        serviceProvided = await xpmDbContext.ServiceProvideds.Where(
          x => x.Project == ProjectDataRequest.ProjectId).ToListAsync();

        projectDataResponse.Named = project.Named;
        projectDataResponse.Number = project.Number;
        projectDataResponse.ContractType = project.ContractType;
        projectDataResponse.ClientOwner = project.ClientOwner;
        projectDataResponse.AddressLine1 = project.AddressLine1;
        projectDataResponse.AddressLine2 = project.AddressLine2;
        projectDataResponse.City = project.City;
        projectDataResponse.Zip = project.Zip;
        projectDataResponse.State = project.State;
        projectDataResponse.ProjectSector = project.ProjectSector;
        projectDataResponse.ProjectType = project.ProjectType;
        projectDataResponse.ProjectSubType = project.ProjectSubType;
        projectDataResponse.ProjectScope = project.ProjectScope;
        projectDataResponse.ProjectDeliveryMethod = project.ProjectDeliveryMethod;
        projectDataResponse.Notes = project.Notes;
        projectDataResponse.Contact = project.Contact;

        foreach (ServiceProvided service in serviceProvided)
        {
          switch (service.AvailableService)
          {
            case 1:
              projectDataResponse.ServiceDesignCriteriaProfessional = true;
              break;
            case 2:
              projectDataResponse.ServiceMasterPlanning = true;
              break;
            case 3:
              projectDataResponse.ServiceArchitecturalDesign = true;
              break;
            case 4:
              projectDataResponse.ServiceProgramming = true;
              break;
            case 5:
              projectDataResponse.ServiceInteriorDesign = true;
              break;
            case 6:
              projectDataResponse.ServiceAsBuiltDocumentation = true;
              break;
            case 7:
              projectDataResponse.ServiceSchematicDesign = true;
              break;
            case 8:
              projectDataResponse.ServiceDesignDevelopment = true;
              break;
            case 9:
              projectDataResponse.ServiceSiteDevelopment = true;
              break;
            case 10:
              projectDataResponse.ServiceConstructionDocuments = true;
              break;
            case 11:
              projectDataResponse.ServiceContractAdministration = true;
              break;
            case 12:
              projectDataResponse.ServiceSitePlanApproval = true;
              break;
            case 13:
              projectDataResponse.ServicePlatting = true;
              break;
            case 14:
              projectDataResponse.ServiceBidding = true;
              break;
            case 15:
              projectDataResponse.ServicePermitting = true;
              break;
            case 16:
              projectDataResponse.ServiceConstructionAssistance = true;
              break;
            case 17:
              projectDataResponse.ServiceEngineering = true;
              break;
            case 18:
              projectDataResponse.ServiceCostEstimating = true;
              break;
            case 19:
              projectDataResponse.ServiceLeedAdministration = true;
              break;
            default:
              break;
          }
        }

        projectDataResponse.ProjectPrincipal = project.ProjectPrincipal;
        projectDataResponse.ProjectManager = project.ProjectManager;
        projectDataResponse.ProjectArchitect = project.ProjectArchitect;
        projectDataResponse.StartDate = project.StartDate;
        projectDataResponse.EndDate = project.EndDate;
        projectDataResponse.ConstructionStartDate = project.ConstructionStartDate;
        projectDataResponse.ConstructionScheduledCompletionDate =
          project.ConstructionScheduledCompletionDate;
        projectDataResponse.ConstructionEndDate = project.ConstructionEndDate;
        projectDataResponse.ConstructionCompany = project.ConstructionCompany;
        projectDataResponse.InitialCost = project.InitialCost;
        projectDataResponse.FinalCost = project.FinalCost;
        projectDataResponse.ServiceFee = project.ServiceFee;
        projectDataResponse.FinalFeesPaid = project.FinalFeesPaid;
        projectDataResponse.CreatedBy = project.CreatedBy;
        projectDataResponse.CreatedOn = project.CreatedOn;
        projectDataResponse.LastUpdatedBy = project.LastUpdatedBy;
        projectDataResponse.LastUpdatedOn = project.LastUpdatedOn;

        projectDataResponse.Success = true;
        return projectDataResponse;
      }
      catch (Exception ex)
      {
        projectDataResponse.Error = "Could not retrieve project data from database. " +
          ex.Message;
        projectDataResponse.ErrorCode = "P19";
        projectDataResponse.Success = false;

        return projectDataResponse;
      }
    }

    public async Task<UpdateProjectResponse> UpdateProjectDataAsync(
      UpdateProjectRequest UpdateProjectRequest)
    {
      UpdateProjectResponse updateProjectResponse = new UpdateProjectResponse();
      Project? project = null;
      List<ServiceProvided>? serviceProvided = null;

      try
      {
        project = await xpmDbContext.Projects.Where(
          x => x.Id == UpdateProjectRequest.Id).SingleAsync();
      }
      catch (Exception ex)
      {
        updateProjectResponse.Error = "Could not confirm project's existence in database. " +
          ex.Message;
        updateProjectResponse.ErrorCode = "P20";
        updateProjectResponse.Success = false;
        return updateProjectResponse;
      }

      try
      {
        serviceProvided = await xpmDbContext.ServiceProvideds.Where(
          x => x.Project == UpdateProjectRequest.Id).ToListAsync();
        foreach (ServiceProvided service in serviceProvided)
        {
          xpmDbContext.Remove(service);
        }
        await xpmDbContext.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        updateProjectResponse.Error = "Could not remove existing services from database. " + 
          ex.Message;
        updateProjectResponse.ErrorCode = "P21";
        updateProjectResponse.Success = false;
        return updateProjectResponse;
      }

      project.Named = UpdateProjectRequest.Named;
      project.Number = UpdateProjectRequest.Number;
      project.ContractType = UpdateProjectRequest.ContractType;
      project.ClientOwner = UpdateProjectRequest.ClientOwner;
      project.AddressLine1 = UpdateProjectRequest.AddressLine1;
      project.AddressLine2 = UpdateProjectRequest.AddressLine2;
      project.City = UpdateProjectRequest.City;
      project.Zip = UpdateProjectRequest.Zip;
      project.State = UpdateProjectRequest.State;
      project.ProjectSector = UpdateProjectRequest.ProjectSector;
      project.ProjectType = UpdateProjectRequest.ProjectType;
      project.ProjectSubType = UpdateProjectRequest.ProjectSubType;
      project.ProjectScope = UpdateProjectRequest.ProjectScope;
      project.ProjectDeliveryMethod = UpdateProjectRequest.ProjectDeliveryMethod;
      project.Notes = UpdateProjectRequest.Notes;
      project.Contact = UpdateProjectRequest.Contact;

      project.ProjectPrincipal = UpdateProjectRequest.ProjectPrincipal;
      project.ProjectManager = UpdateProjectRequest.ProjectManager;
      project.ProjectArchitect = UpdateProjectRequest.ProjectArchitect;
      project.StartDate = UpdateProjectRequest.StartDate;
      project.EndDate = UpdateProjectRequest.EndDate;
      project.ConstructionStartDate = UpdateProjectRequest.ConstructionStartDate;
      project.ConstructionScheduledCompletionDate =
        UpdateProjectRequest.ConstructionScheduledCompletionDate;
      project.ConstructionEndDate = UpdateProjectRequest.ConstructionEndDate;
      project.ConstructionCompany = UpdateProjectRequest.ConstructionCompany;
      project.InitialCost = UpdateProjectRequest.InitialCost;
      project.FinalCost = UpdateProjectRequest.FinalCost;
      project.ServiceFee = UpdateProjectRequest.ServiceFee;
      project.FinalFeesPaid = UpdateProjectRequest.FinalFeesPaid;
      
      project.LastUpdatedBy = 13;
      project.LastUpdatedOn = DateTime.UtcNow;

      try
      {
        xpmDbContext.Update(project);
        await xpmDbContext.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        updateProjectResponse.Error = "Could not update project information to database. " +
          ex.Message;
        updateProjectResponse.ErrorCode = "P22";
        updateProjectResponse.Success = false;
        return updateProjectResponse;
      }

      try
      {
        if (!await UpdateProjectServiceInformationAsync(UpdateProjectRequest.Id,
          UpdateProjectRequest))
          throw new Exception();
      }
      catch (Exception ex)
      {
        updateProjectResponse.Error =
          "Updated project service insert did not complete successfully. " +
          ex.Message;
        updateProjectResponse.ErrorCode = "P23";
        updateProjectResponse.Success = false;
        return updateProjectResponse;
      }
      
      updateProjectResponse.Success = true;
      return updateProjectResponse;
    }

    

    public async Task<LookupListResponse> GetSectorTypeAsync(
      SectorTypeRequest SectorTypeRequest)
    {
      LookupListResponse response = new LookupListResponse();
      List<ProjectType> projectType;
      LookupItem lookupItem;

      try
      {
        projectType = await xpmDbContext.ProjectTypes
          .Where(x => x.ProjectSector == SectorTypeRequest.SectorId)
          .OrderBy(x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project type list from database. " +
          ex.Message;
        response.ErrorCode = "P18";
        response.Success = false;

        return response;
      }

      if (projectType != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ProjectType dbLookupItem in projectType)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetTypeSubTypeAsync(
      TypeSubTypeRequest TypeSubTypeRequest)
    {
      LookupListResponse response = new LookupListResponse();
      List<ProjectSubType>? projectSubType = null;
      LookupItem lookupItem;

      try
      {
        projectSubType = await xpmDbContext.ProjectSubTypes
          .Where(x => x.ProjectType == TypeSubTypeRequest.TypeId)
          .OrderBy(x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project sub-type list from database. " +
          ex.Message;
        response.ErrorCode = "P17";
        response.Success = false;

        return response;
      }

      if (projectSubType != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ProjectSubType dbLookupItem in projectSubType)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<ContactPersonDataResponse> GetContactPersonInformationAsync(
      ContactPersonDataRequest ContactPersonDataRequest)
    {
      Contact? contact = null;
      ContactPersonDataResponse contactPersonDataResponse = new ContactPersonDataResponse();

      try
      {
        contact = await xpmDbContext.Contacts.Where(
          x => x.Id == ContactPersonDataRequest.ContactPersonId).SingleOrDefaultAsync();
        if (contact != null)
        {
          contactPersonDataResponse = new ContactPersonDataResponse();
          contactPersonDataResponse.PositionJobTitle = contact.Title;
          contactPersonDataResponse.PhoneNumber = contact.PhoneNumber;
          contactPersonDataResponse.FaxNumber = contact.FaxNumber;
          contactPersonDataResponse.EmailAddress = contact.EmailAddress;
          contactPersonDataResponse.Success = true;
        }
      }
      catch (Exception ex)
      {
        contactPersonDataResponse.Error = "Could not retrieve contact data. " + ex.Message;
        contactPersonDataResponse.ErrorCode = "P16";
        contactPersonDataResponse.Success = false;
      }
      return contactPersonDataResponse;
    }

    public async Task<LookupListResponse> GetContractTypeListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ContractType> contractType;
      LookupItem lookupItem;

      try
      {
        contractType = await xpmDbContext.ContractTypes.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve contract type list from database. " +
          ex.Message;
        response.ErrorCode = "P14";
        response.Success = false;

        return response;
      }

      if (contractType != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ContractType dbLookupItem in contractType)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetProjectArchitectListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ProjectArchitect> projectArchitect;
      LookupItem lookupItem;

      try
      {
        projectArchitect = await xpmDbContext.ProjectArchitects.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project architect list from database. " +
          ex.Message;
        response.ErrorCode = "P13";
        response.Success = false;

        return response;
      }

      if (projectArchitect != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ProjectArchitect dbLookupItem in projectArchitect)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetProjectManagerListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ProjectManager> projectManager;
      LookupItem lookupItem;

      try
      {
        projectManager = await xpmDbContext.ProjectManagers.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project manager list from database. " +
          ex.Message;
        response.ErrorCode = "P12";
        response.Success = false;

        return response;
      }

      if (projectManager != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ProjectManager dbLookupItem in projectManager)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetProjectPrincipalListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ProjectPrincipal> projectPrincipal;
      LookupItem lookupItem;

      try
      {
        projectPrincipal = await xpmDbContext.ProjectPrincipals.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project principal list from database. " +
          ex.Message;
        response.ErrorCode = "P11";
        response.Success = false;

        return response;
      }

      if (projectPrincipal != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ProjectPrincipal dbLookupItem in projectPrincipal)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetConstructionCompanyListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ConstructionCompany> constructionCompany;
      LookupItem lookupItem;

      try
      {
        constructionCompany = await xpmDbContext.ConstructionCompanies.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve construction company list from database. " +
          ex.Message;
        response.ErrorCode = "P10";
        response.Success = false;

        return response;
      }

      if (constructionCompany != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ConstructionCompany dbLookupItem in constructionCompany)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetStateListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<State> state;
      LookupItem lookupItem;

      try
      {
        state = await xpmDbContext.States.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve state list from database. " +
          ex.Message;
        response.ErrorCode = "P09";
        response.Success = false;

        return response;
      }

      if (state != null)
      {
        response.Item = new List<LookupItem>();

        foreach (State dbLookupItem in state)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetContactListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<Contact> contact;
      LookupItem lookupItem;

      try
      {
        contact = await xpmDbContext.Contacts.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project contact list from database. " +
          ex.Message;
        response.ErrorCode = "P08";
        response.Success = false;

        return response;
      }

      if (contact != null)
      {
        response.Item = new List<LookupItem>();

        foreach (Contact dbLookupItem in contact)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetProjectDeliveryMethodListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ProjectDeliveryMethod> projectDeliveryMethod;
      LookupItem lookupItem;

      try
      {
        projectDeliveryMethod = await xpmDbContext.ProjectDeliveryMethods.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project delivery method list from database. " +
          ex.Message;
        response.ErrorCode = "P07";
        response.Success = false;

        return response;
      }

      if (projectDeliveryMethod != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ProjectDeliveryMethod dbLookupItem in projectDeliveryMethod)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetProjectSubTypeListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ProjectSubType> projectSubType;
      LookupItem lookupItem;

      try
      {
        projectSubType = await xpmDbContext.ProjectSubTypes.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project sub type list from database. " +
          ex.Message;
        response.ErrorCode = "P06";
        response.Success = false;

        return response;
      }

      if (projectSubType != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ProjectSubType dbLookupItem in projectSubType)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetProjectTypeListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ProjectType> projectType;
      LookupItem lookupItem;

      try
      {
        projectType = await xpmDbContext.ProjectTypes.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project type list from database. " +
          ex.Message;
        response.ErrorCode = "P05";
        response.Success = false;

        return response;
      }

      if (projectType != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ProjectType dbLookupItem in projectType)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetProjectSectorListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ProjectSector> projectSector;
      LookupItem lookupItem;

      try
      {
        projectSector = await xpmDbContext.ProjectSectors.OrderBy(
          x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project sector list from database. " +
          ex.Message;
        response.ErrorCode = "P04";
        response.Success = false;

        return response;
      }

      if (projectSector != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ProjectSector dbLookupItem in projectSector)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetProjectScopeListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ProjectScope> projectScope;
      LookupItem lookupItem;

      try
      {
        projectScope = await xpmDbContext.ProjectScopes.OrderBy(x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project scope list from database. " +
          ex.Message;
        response.ErrorCode = "P03";
        response.Success = false;

        return response;
      }

      if (projectScope != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ProjectScope dbLookupItem in projectScope)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<LookupListResponse> GetClientOwnerListAsync()
    {
      LookupListResponse response = new LookupListResponse();
      List<ClientOwner> clientOwner;
      LookupItem lookupItem;

      try
      {
        clientOwner = await xpmDbContext.ClientOwners.OrderBy(x => x.Named).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve client/owner list from database. " +
          ex.Message;
        response.ErrorCode = "P02";
        response.Success = false;

        return response;
      }

      if (clientOwner != null)
      {
        response.Item = new List<LookupItem>();

        foreach (ClientOwner dbLookupItem in clientOwner)
        {
          lookupItem = new LookupItem();
          lookupItem.Id = dbLookupItem.Id;
          lookupItem.Named = dbLookupItem.Named;
          response.Item.Add(lookupItem);
        }
      }
      response.Success = true;
      return response;
    }

    public async Task<ProjectListResponse> GetProjectListAsync()
    {
      ProjectListResponse response = new ProjectListResponse();
      List<Project> project;
      ProjectListProject projectListProject;
      
      try
      {
        project = await xpmDbContext.Projects.Include(
          p => p.ProjectSubTypeNavigation).ToListAsync();
      }
      catch (Exception ex)
      {
        response.Error = "Could not retrieve project list from database. " +
          ex.Message;
        response.ErrorCode = "P01";
        response.Success = false;
        
        return response;
      }
      
      if (project != null)
      {
        response.Project = new List<ProjectListProject>();

        foreach (Project dbproject in project)
        {
          projectListProject = new ProjectListProject();
          projectListProject.Id = dbproject.Id;
          projectListProject.Number = dbproject.Number;
          projectListProject.Name = dbproject.Named;
          projectListProject.EndDate = dbproject.EndDate;
          projectListProject.LastUpdatedBy = 
            await GetUserEmailAddress(dbproject.LastUpdatedBy);
          projectListProject.LastUpdatedOn = dbproject.LastUpdatedOn;
          response.Project.Add(projectListProject);
        }
      }
      response.Success = true;
      return response;
    }

    private async Task<bool> PostNewProjectServiceInformationAsync(int ProjectId,
      PostNewProjectRequest PostNewProjectRequest)
    {
      if (PostNewProjectRequest.ServiceDesignCriteriaProfessional != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceDesignCriteriaProfessional, 1))
          return false;
      if (PostNewProjectRequest.ServiceMasterPlanning != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceMasterPlanning, 2))
          return false;
      if (PostNewProjectRequest.ServiceArchitecturalDesign != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceArchitecturalDesign, 3))
          return false;
      if (PostNewProjectRequest.ServiceProgramming != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceProgramming, 4))
          return false;
      if (PostNewProjectRequest.ServiceInteriorDesign != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceInteriorDesign, 5))
          return false;
      if (PostNewProjectRequest.ServiceAsBuiltDocumentation != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceAsBuiltDocumentation, 6))
          return false;
      if (PostNewProjectRequest.ServiceSchematicDesign != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceSchematicDesign, 7))
          return false;
      if (PostNewProjectRequest.ServiceDesignDevelopment != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceDesignDevelopment, 8))
          return false;
      if (PostNewProjectRequest.ServiceSiteDevelopment != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceSiteDevelopment, 9))
          return false;
      if (PostNewProjectRequest.ServiceConstructionDocuments != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceConstructionDocuments, 10))
          return false;
      if (PostNewProjectRequest.ServiceContractAdministration != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceContractAdministration, 11))
          return false;
      if (PostNewProjectRequest.ServiceSitePlanApproval != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceSitePlanApproval, 12))
          return false;
      if (PostNewProjectRequest.ServicePlatting != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServicePlatting, 13))
          return false;
      if (PostNewProjectRequest.ServiceBidding != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceBidding, 14))
          return false;
      if (PostNewProjectRequest.ServicePermitting != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServicePermitting, 15))
          return false;
      if (PostNewProjectRequest.ServiceConstructionAssistance != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceConstructionAssistance, 16))
          return false;
      if (PostNewProjectRequest.ServiceEngineering != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceEngineering, 17))
          return false;
      if (PostNewProjectRequest.ServiceCostEstimating != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceCostEstimating, 18))
          return false;
      if (PostNewProjectRequest.ServiceLeedAdministration != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)PostNewProjectRequest.ServiceLeedAdministration, 19))
          return false;
      await xpmDbContext.SaveChangesAsync();
      return true;
    }

    private async Task<bool> UpdateProjectServiceInformationAsync(int ProjectId,
      UpdateProjectRequest UpdateProjectRequest)
    {
      if (UpdateProjectRequest.ServiceDesignCriteriaProfessional != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceDesignCriteriaProfessional, 1))
          return false;
      if (UpdateProjectRequest.ServiceMasterPlanning != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceMasterPlanning, 2))
          return false;
      if (UpdateProjectRequest.ServiceArchitecturalDesign != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceArchitecturalDesign, 3))
          return false;
      if (UpdateProjectRequest.ServiceProgramming != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceProgramming, 4))
          return false;
      if (UpdateProjectRequest.ServiceInteriorDesign != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceInteriorDesign, 5))
          return false;
      if (UpdateProjectRequest.ServiceAsBuiltDocumentation != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceAsBuiltDocumentation, 6))
          return false;
      if (UpdateProjectRequest.ServiceSchematicDesign != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceSchematicDesign, 7))
          return false;
      if (UpdateProjectRequest.ServiceDesignDevelopment != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceDesignDevelopment, 8))
          return false;
      if (UpdateProjectRequest.ServiceSiteDevelopment != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceSiteDevelopment, 9))
          return false;
      if (UpdateProjectRequest.ServiceConstructionDocuments != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceConstructionDocuments, 10))
          return false;
      if (UpdateProjectRequest.ServiceContractAdministration != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceContractAdministration, 11))
          return false;
      if (UpdateProjectRequest.ServiceSitePlanApproval != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceSitePlanApproval, 12))
          return false;
      if (UpdateProjectRequest.ServicePlatting != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServicePlatting, 13))
          return false;
      if (UpdateProjectRequest.ServiceBidding != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceBidding, 14))
          return false;
      if (UpdateProjectRequest.ServicePermitting != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServicePermitting, 15))
          return false;
      if (UpdateProjectRequest.ServiceConstructionAssistance != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceConstructionAssistance, 16))
          return false;
      if (UpdateProjectRequest.ServiceEngineering != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceEngineering, 17))
          return false;
      if (UpdateProjectRequest.ServiceCostEstimating != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceCostEstimating, 18))
          return false;
      if (UpdateProjectRequest.ServiceLeedAdministration != null)
        if (!await AddServiceProvidedAsync(ProjectId,
          (bool)UpdateProjectRequest.ServiceLeedAdministration, 19))
          return false;
      await xpmDbContext.SaveChangesAsync();
      return true;
    }

    private async Task<bool> AddServiceProvidedAsync(int ProjectId,
      bool ServiceProvided, int AvailableService)
    {
      ServiceProvided? serviceProvided = null;
      try
      {
        if (ServiceProvided)
        {
          serviceProvided = new ServiceProvided()
          {
            Project = ProjectId,
            AvailableService = AvailableService
          };
          await xpmDbContext.AddAsync(serviceProvided);
          return true;
        }
      }
      catch
      {
        return false;
      }
      return true;
    }

    private async Task<String> GetUserEmailAddress(int? UserId)
    {
      AuthUser? user = null;

      try
      {
        if (UserId == null)
          return String.Empty;
        user = await xpmDbContext.AuthUsers.Where(
          x => x.Id == UserId).SingleAsync();      
        if (user != null)
          return user.EmailAddress;
        return String.Empty;
      }
      catch
      {
        return String.Empty;
      }
    }
  }
}