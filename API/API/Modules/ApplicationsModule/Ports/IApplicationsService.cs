using API.Infrastructure;
using API.Modules.ApplicationsModule.DTO;
using API.Modules.ProfilesModule.Entity;

namespace API.Modules.ApplicationsModule.Ports;

public interface IApplicationsService
{
  public Result<IEnumerable<ApplicationOutDTO>> GetApplicationsAsync();
  public Task<Result<Guid>> CreateApplicationAsync(Guid ownerId, ApplicationInnerDTO applicationInner);
  public Task<Result<bool>> UpdateApplicationAsync(Guid userId, Guid applicationId, ApplicationInnerDTO applicationInner);

  public Task<Result<bool>> SuggestWorkersAsync(Guid applicationId, IEnumerable<ProfileEntity> workers);
}
