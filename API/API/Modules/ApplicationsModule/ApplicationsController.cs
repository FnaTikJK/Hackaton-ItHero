using API.Infrastructure;
using API.Modules.AccountsModule.Entity;
using API.Modules.ApplicationsModule.DTO;
using API.Modules.ApplicationsModule.Ports;
using API.Modules.ProfilesModule.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.ApplicationsModule;

[Route("api/[controller]")]
[ApiController]
public class ApplicationsController : ControllerBase
{
  private readonly IApplicationsService applicationsService;

  public ApplicationsController(IApplicationsService applicationsService)
  {
    this.applicationsService = applicationsService;
  }

  [HttpGet]
  [Authorize]
  public ActionResult<IEnumerable<ApplicationOutDTO>> GetApplicationsAsync()
  {
    var response = applicationsService.GetApplicationsAsync();

    return response.IsSuccess ? Ok(response.Value)
      : BadRequest(response.Error);
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult> CreateApplicationAsync(ApplicationInnerDTO applicationInner)
  {
    var response = await applicationsService.CreateApplicationAsync(User.GetId(), applicationInner)
      .ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult> UpdateApplicationAsync(Guid applicationId, ApplicationInnerDTO applicationInner)
  {
    var response = await applicationsService.UpdateApplicationAsync(User.GetId(), applicationId, applicationInner)
      .ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  [HttpPost]
  [Authorize(nameof(AccountRole.Admin))]
  public async Task<ActionResult> SuggestWorkersAsync(Guid applicationId, HashSet<ProfileEntity> workers)
  {
    var response = await applicationsService.SuggestWorkersAsync(applicationId, workers).ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }
}
