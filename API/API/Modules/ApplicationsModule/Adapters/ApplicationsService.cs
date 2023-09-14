using API.DAL;
using API.Infrastructure;
using API.Modules.ApplicationsModule.DTO;
using API.Modules.ApplicationsModule.Entity;
using API.Modules.ApplicationsModule.Ports;
using API.Modules.ProfilesModule.Entity;
using AutoMapper;

namespace API.Modules.ApplicationsModule.Adapters;

public class ApplicationsService : IApplicationsService
{
  private readonly DataContext dataContext;
  private readonly IMapper mapper;

  public ApplicationsService(DataContext dataContext, IMapper mapper)
  {
    this.dataContext = dataContext;
    this.mapper = mapper;
  }
  public Result<IEnumerable<ApplicationOutDTO>> GetApplicationsAsync()
  {
    var applications = dataContext.Applications.ToList();

    return Result.Ok(mapper.Map<IEnumerable<ApplicationOutDTO>>(applications));
  }

  public async Task<Result<Guid>> CreateApplicationAsync(Guid ownerId, ApplicationInnerDTO applicationInner)
  {
    var application = mapper.Map<ApplicationEntity>(applicationInner);
    application.OwnerId = ownerId;
    await dataContext.Applications.AddAsync(application).ConfigureAwait(false);
    return Result.Ok(application.Id);
  }

  public async Task<Result<bool>> UpdateApplicationAsync(Guid userId, Guid applicationId, ApplicationInnerDTO applicationInner)
  {
    var application = await dataContext.Applications.FindAsync(applicationId).ConfigureAwait(false);
    if (application == null)
      return Result.Fail<bool>("Такой заявки не существует");

    if (application.OwnerId != userId)
      return Result.Fail<bool>("У вас нет прав");

    mapper.Map(applicationInner, application);
    await dataContext.SaveChangesAsync().ConfigureAwait(false);

    return Result.Ok(true);
  }

  public async Task<Result<bool>> SuggestWorkersAsync(Guid applicationId, IEnumerable<ProfileEntity> workers)
  {
    var application = await dataContext.Applications.FindAsync(applicationId).ConfigureAwait(false);
    if (application == null)
      return Result.Fail<bool>("Такой заявки не существует");
    application.SuggestedExecutors.UnionWith(workers);
    await dataContext.SaveChangesAsync().ConfigureAwait(false);

    return Result.Ok(true);
  }
}
