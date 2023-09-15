
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

  [HttpPut("{applicationId}")]
  [Authorize]
  public async Task<ActionResult> UpdateApplicationAsync(Guid applicationId, ApplicationInnerDTO applicationInner)
  {
    var response = await applicationsService.UpdateApplicationAsync(User.GetId(), applicationId, applicationInner)
      .ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  [HttpPost("{applicationId}/Suggest")]
  [Authorize("Admin")]
  public async Task<ActionResult> SuggestWorkersAsync(Guid applicationId, WorkersDTO workersDto)
  {
    var response = await applicationsService.SuggestWorkersAsync(applicationId, workersDto.WorkersId).ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  [HttpPost("{applicationId}/Hire")]
  public async Task<ActionResult> HireWorkersAsync(Guid applicationId, WorkersDTO workersDto)
  {
    var response = await applicationsService.HireWorkersAsync(applicationId, workersDto.WorkersId).ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  [HttpPost("{applicationId}/Invite")]
  public async Task<ActionResult> InviteWorkersAsync(Guid applicationId, WorkersDTO workersDto)
  {
    var response = await applicationsService.InviteWorkersAsync(applicationId, workersDto.WorkersId).ConfigureAwait(false);

    return response.IsSuccess ? NoContent()
      : BadRequest(response.Error);
  }

  public class WorkersDTO
  {
    public HashSet<Guid> WorkersId { get; set; }
  }

  [HttpPost("{applicationId}/Reject")]
  public async Task<ActionResult> InviteWorkersAsync(Guid applicationId, [FromBody]WorkerDTO workerDto)
  {
    await applicationsService.RemoveWorker(applicationId, workerDto.WorkerId);

    return NoContent();
  }

  public class WorkerDTO
  {
    public Guid WorkerId { get; set; }
  }
}
