
using API.Infrastructure;
using API.Modules.ApplicationsModule.DTO;
using API.Modules.ApplicationsModule.Ports;
using API.Modules.ProfilesModule.DTO;
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

  [HttpGet("My")]
  [Authorize]
  public ActionResult<IEnumerable<ApplicationOutDTO>> GetApplicationsAsync()
  {
    var response = applicationsService.GetApplicationsAsync(User.GetId());

    return response.IsSuccess ? Ok(response.Value)
      : BadRequest(response.Error);
  }

  [HttpPost("Create")]
  [Authorize]
  public async Task<ActionResult> CreateApplicationAsync(ApplicationInnerDTO applicationInner)
  {
    var response = await applicationsService.CreateApplicationAsync(User.GetId(), applicationInner)
      .ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  [HttpPost("Remove")]
  [Authorize]
  public async Task<ActionResult> RemoveApplicationAsync(Guid applicationId)
  {
    var response = await applicationsService.RemoveApplicationAsync(User.GetId(), applicationId)
      .ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  [HttpPut("Update")]
  [Authorize]
  public async Task<ActionResult> UpdateApplicationAsync(Guid applicationId, ApplicationInnerDTO applicationInner)
  {
    var response = await applicationsService.UpdateApplicationAsync(User.GetId(), applicationId, applicationInner)
      .ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  [HttpPost("Suggest")]
  [Authorize("Admin")]
  public async Task<ActionResult> SuggestWorkersAsync(Guid applicationId, HashSet<Guid> workers)
  {
    var response = await applicationsService.SuggestWorkersAsync(applicationId, workers).ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  [HttpPost("Hire")]
  public async Task<ActionResult> HireWorkersAsync(Guid applicationId, HashSet<Guid> workers)
  {
    var response = await applicationsService.HireWorkersAsync(applicationId, workers).ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  [HttpPost("Invite")]
  public async Task<ActionResult> InviteWorkersAsync(Guid applicationId, HashSet<Guid> workers)
  {
    var response = await applicationsService.InviteWorkersAsync(applicationId, workers).ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }
}
