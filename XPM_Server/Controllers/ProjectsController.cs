using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XPM.Server.Interfaces;
using XPM.Server.Requests;
using XPM.Server.Responses;

namespace XPM.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProjectsController : BaseApiController
  {
    private readonly IProjectService? projectService;

    public ProjectsController(IProjectService ProjectService)
    {
      this.projectService = ProjectService;
    }

    [Authorize]
    [HttpPut]
    [Route("update_project")]
    public async Task<IActionResult> UpdateProject(
      UpdateProjectRequest UpdateProjectRequest)
    {
      if (projectService != null)
      {
        var updateProjectResponse =
          await projectService.UpdateProjectDataAsync(UpdateProjectRequest);
        if (updateProjectResponse.Success)
        {
          return Ok(updateProjectResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = updateProjectResponse.Error,
            ErrorCode = updateProjectResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("project_information/{id}")]
    public async Task<IActionResult> GetProjectInformation(int id)
    {
      ProjectDataRequest projectDataRequest = 
        new ProjectDataRequest { ProjectId = id };

      if (projectService != null)
      {
        var projectDataResponse = 
          await projectService.GetProjectDataAsync(projectDataRequest);
        if (projectDataResponse.Success)
        {
          return Ok(projectDataResponse);
        }
        else
        {
          return BadRequest(new ProjectDataResponse
          {
            Error = projectDataResponse.Error,
            ErrorCode = projectDataResponse.ErrorCode
          });
        }
      }
      return BadRequest(new ProjectDataResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("project_type_list/{id}")]
    public async Task<IActionResult> ProjectTypeList(int? id)
    {
      SectorTypeRequest? sectorTypeRequest = null;
      LookupListResponse? projectTypeListResponse = null;

      if (id != null)
        sectorTypeRequest = new SectorTypeRequest { SectorId = (int)id };

      if (projectService != null)
      {
        if (sectorTypeRequest != null)
        {
          projectTypeListResponse =
            await projectService.GetSectorTypeAsync(sectorTypeRequest);
          projectTypeListResponse.Success = true;
        }
        else
        {
          projectTypeListResponse = new LookupListResponse();
          projectTypeListResponse.Success = true;
        }
        
        if (projectTypeListResponse.Success)
        {
          return Ok(projectTypeListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = projectTypeListResponse.Error,
            ErrorCode = projectTypeListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("project_sub_type_list/{id}")]
    public async Task<IActionResult> ProjectSubTypeList(int? id)
    {
      TypeSubTypeRequest? typeSubTypeRequest = null;
      LookupListResponse? projectTypeSubTypeListResponse = null;

      if (id != null)
        typeSubTypeRequest = new TypeSubTypeRequest { TypeId = (int)id };

      if (projectService != null)
      {
        if (typeSubTypeRequest != null)
        {
          projectTypeSubTypeListResponse =
            await projectService.GetTypeSubTypeAsync(typeSubTypeRequest);
          projectTypeSubTypeListResponse.Success = true;
        }
        else
        {
          projectTypeSubTypeListResponse = new LookupListResponse();
          projectTypeSubTypeListResponse.Success = true;
        }

        if (projectTypeSubTypeListResponse.Success)
        {
          return Ok(projectTypeSubTypeListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = projectTypeSubTypeListResponse.Error,
            ErrorCode = projectTypeSubTypeListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("contact_information/{id}")]
    public async Task<IActionResult> GetContactInformation(int id)
    {
      ContactPersonDataRequest contactPersonDataRequest = 
        new ContactPersonDataRequest { ContactPersonId = id };

      if (projectService != null)
      {
        var contactPersonDataResponse =
          await projectService.GetContactPersonInformationAsync(contactPersonDataRequest);
        if (contactPersonDataResponse.Success)
        {
          return Ok(contactPersonDataResponse);
        }
        else
        {
          return BadRequest(new ContactPersonDataResponse
          {
            Error = contactPersonDataResponse.Error,
            ErrorCode = contactPersonDataResponse.ErrorCode
          });
        }
      }
      return BadRequest(new ContactPersonDataResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02" 
      });
    }

    [Authorize]
    [HttpPost]
    [Route("new_project")]
    public async Task<IActionResult> AddNewProject(
      PostNewProjectRequest PostNewProjectRequest)
    {
      if (projectService != null) 
      {
        var postNewProjectResponse =
          await projectService.PostNewProjectDataAsync(PostNewProjectRequest);
        if (postNewProjectResponse.Success) 
        {
          return Ok(postNewProjectResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = postNewProjectResponse.Error,
            ErrorCode = postNewProjectResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("contract_type_list")]
    public async Task<IActionResult> ContractTypeList()
    {
      if (projectService != null)
      {
        var contractTypeListResponse =
          await projectService.GetContractTypeListAsync();

        if (contractTypeListResponse.Success)
        {
          return Ok(contractTypeListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = contractTypeListResponse.Error,
            ErrorCode = contractTypeListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("project_architect_list")]
    public async Task<IActionResult> ProjectArchitectlList()
    {
      if (projectService != null)
      {
        var projectArchitectListResponse =
          await projectService.GetProjectArchitectListAsync();

        if (projectArchitectListResponse.Success)
        {
          return Ok(projectArchitectListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = projectArchitectListResponse.Error,
            ErrorCode = projectArchitectListResponse.Error
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("project_manager_list")]
    public async Task<IActionResult> ProjectManagerlList()
    {
      if (projectService != null)
      {
        var projectPrincipalListResponse =
          await projectService.GetProjectManagerListAsync();

        if (projectPrincipalListResponse.Success)
        {
          return Ok(projectPrincipalListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = projectPrincipalListResponse.Error,
            ErrorCode = projectPrincipalListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("project_principal_list")]
    public async Task<IActionResult> ProjectPrincipalList()
    {
      if (projectService != null)
      {
        var projectPrincipalListResponse =
          await projectService.GetProjectPrincipalListAsync();

        if (projectPrincipalListResponse.Success)
        {
          return Ok(projectPrincipalListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = projectPrincipalListResponse.Error,
            ErrorCode = projectPrincipalListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("construction_company_list")]
    public async Task<IActionResult> ConstructionCompanyList()
    {
      if (projectService != null)
      {
        var constructionCompanyListResponse =
          await projectService.GetConstructionCompanyListAsync();

        if (constructionCompanyListResponse.Success)
        {
          return Ok(constructionCompanyListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = constructionCompanyListResponse.Error,
            ErrorCode = constructionCompanyListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("contact_list")]
    public async Task<IActionResult> ContactList()
    {
      if (projectService != null)
      {
        var contactListResponse =
          await projectService.GetContactListAsync();

        if (contactListResponse.Success)
        {
          return Ok(contactListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = contactListResponse.Error,
            ErrorCode = contactListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("project_delivery_method_list")]
    public async Task<IActionResult> ProjectDeliveryMethodList()
    {
      if (projectService != null)
      {
        var projectDeliveryMethodListResponse = 
          await projectService.GetProjectDeliveryMethodListAsync();

        if (projectDeliveryMethodListResponse.Success)
        {
          return Ok(projectDeliveryMethodListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = projectDeliveryMethodListResponse.Error,
            ErrorCode = projectDeliveryMethodListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("project_sector_list")]
    public async Task<IActionResult> ProjectSectorList()
    {
      if (projectService != null)
      {
        var projectSectorListResponse = await projectService.GetProjectSectorListAsync();

        if (projectSectorListResponse.Success)
        {
          return Ok(projectSectorListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = projectSectorListResponse.Error,
            ErrorCode = projectSectorListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("project_scope_list")]
    public async Task<IActionResult> ProjectScopeList()
    {
      if (projectService != null)
      {
        var projectScopeListResponse = await projectService.GetProjectScopeListAsync();

        if (projectScopeListResponse.Success)
        {
          return Ok(projectScopeListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = projectScopeListResponse.Error,
            ErrorCode = projectScopeListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("state_list")]
    public async Task<IActionResult> StateList()
    {
      if (projectService != null)
      {
        var stateListResponse = await projectService.GetStateListAsync();

        if (stateListResponse.Success)
        {
          return Ok(stateListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = stateListResponse.Error,
            ErrorCode = stateListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("client_owner_list")]
    public async Task<IActionResult> ClientOwnerList()
    {
      if (projectService != null)
      {
        var clientOwnerListResponse = await projectService.GetClientOwnerListAsync();

        if (clientOwnerListResponse.Success)
        {
          return Ok(clientOwnerListResponse);
        }
        else
        {
          return BadRequest(new LookupListResponse
          {
            Error = clientOwnerListResponse.Error,
            ErrorCode = clientOwnerListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new LookupListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }

    [Authorize]
    [HttpGet]
    [Route("project_list")]
    public async Task<IActionResult> ProjectList()
    { 
      if (projectService != null) {
        var projectListResponse = await projectService.GetProjectListAsync();

        if (projectListResponse.Success)
        {
          return Ok(projectListResponse);
        }
        else
        {
          return BadRequest(new ProjectListResponse
          {
            Error = projectListResponse.Error,
            ErrorCode = projectListResponse.ErrorCode
          });
        }
      }
      return BadRequest(new ProjectListResponse
      {
        Error = "Project service unavailable.",
        ErrorCode = "P02"
      });
    }
  }
}
