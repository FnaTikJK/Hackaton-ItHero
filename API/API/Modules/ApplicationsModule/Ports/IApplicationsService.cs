using API.Infrastructure;
using API.Modules.ApplicationsModule.DTO;
using API.Modules.ProfilesModule.DTO;
using API.Modules.ProfilesModule.Entity;

namespace API.Modules.ApplicationsModule.Ports;

public interface IApplicationsService
{
  public Result<IEnumerable<ApplicationOutDTO>> GetApplicationsAsync(Guid ownerId);
  public Task<Result<Guid>> CreateApplicationAsync(Guid ownerId, ApplicationInnerDTO applicationInner);

  public Task<Result<bool>> RemoveApplicationAsync(Guid ownerId, Guid applicationId);
  public Task<Result<bool>> UpdateApplicationAsync(Guid userId, Guid applicationId, ApplicationInnerDTO applicationInner);

  public Task<Result<bool>> SuggestWorkersAsync(Guid applicationId, IEnumerable<Guid> workersIds);

  public Task<Result<bool>> InviteWorkersAsync(Guid applicationId, IEnumerable<Guid> workersIds);

  public Task<Result<bool>> HireWorkersAsync(Guid applicationId, IEnumerable<Guid> workerId);
  Task RemoveWorker(Guid applicationId, Guid workerId);
}
