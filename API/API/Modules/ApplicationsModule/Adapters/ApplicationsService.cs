using API.DAL;
using API.Infrastructure;
using API.Modules.ApplicationsModule.DTO;
using API.Modules.ApplicationsModule.Entity;
using API.Modules.ApplicationsModule.Ports;
using API.Modules.ProfilesModule.Entity;
using AutoMapper;

namespace API.Modules.ApplicationsModule.Adapters;

public class  ApplicationsService : IApplicationsService
{
  private readonly DataContext dataContext;
  private readonly IMapper mapper;

  public ApplicationsService(DataContext dataContext, IMapper mapper)
  {
    this.dataContext = dataContext;
    this.mapper = mapper;
  }
  public Result<IEnumerable<ApplicationOutDTO>> GetApplicationsAsync(Guid ownerId)
  {
    var applications = dataContext.Applications.Where(app => app.OwnerId == ownerId);
    var y = dataContext.Applications.ToList();
    var x = applications.ToList();
    return Result.Ok(mapper.Map<IEnumerable<ApplicationOutDTO>>(applications));
  }

  public async Task<Result<Guid>> CreateApplicationAsync(Guid ownerId, ApplicationInnerDTO applicationInner)
  {
    var application = mapper.Map<ApplicationEntity>(applicationInner);
    application.ExpiryAt = DateTime.UtcNow + TimeSpan.FromDays(90);
    application.Id = Guid.NewGuid();
    application.OwnerId = ownerId;
    await dataContext.Applications.AddAsync(application).ConfigureAwait(false);
    await dataContext.SaveChangesAsync().ConfigureAwait(false);
    return Result.Ok(application.Id);
  }

  public async Task<Result<bool>> RemoveApplicationAsync(Guid ownerId, Guid applicationId)
  {
    var application = await dataContext.Applications.FindAsync(applicationId).ConfigureAwait(false);
    if (application == null)
      return Result.Fail<bool>("Такой заявки не существует");
    if (application.OwnerId != ownerId)
      return Result.Fail<bool>("У вас нет прав");
    dataContext.Applications.Remove(application);
    await dataContext.SaveChangesAsync().ConfigureAwait(false);

    return Result.Ok(true);
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

  public async Task<Result<bool>> SuggestWorkersAsync(Guid applicationId, IEnumerable<Guid> workersIds)
  {
    var application = await dataContext.Applications.FindAsync(applicationId).ConfigureAwait(false);
    var workers =  await GetWorkersAsync(workersIds).ConfigureAwait(false);
    if (application == null)
      return Result.Fail<bool>("Такой заявки не существует");
    application.SuggestedExecutors ??= new HashSet<ProfileEntity>();
    application.SuggestedExecutors.UnionWith(workers);
    await dataContext.SaveChangesAsync().ConfigureAwait(false);

    return Result.Ok(true);
  }

  public async Task<Result<bool>> InviteWorkersAsync(Guid applicationId, IEnumerable<Guid> workersIds)
  {
    var application = await dataContext.Applications.FindAsync(applicationId).ConfigureAwait(false);
    var workers = await GetWorkersAsync(workersIds).ConfigureAwait(false);
    if (application == null)
      return Result.Fail<bool>("Такой заявки не существует");

    application.InvitedExecutors ??= new HashSet<ProfileEntity>();
    application.InvitedExecutors.UnionWith(workers);
    await dataContext.SaveChangesAsync().ConfigureAwait(false);

    return Result.Ok(true);
  }

  public async Task<Result<bool>> HireWorkersAsync(Guid applicationId, IEnumerable<Guid> workerId)
  {
    var application = await dataContext.Applications.FindAsync(applicationId).ConfigureAwait(false);
    var workers = await GetWorkersAsync(workerId).ConfigureAwait(false);
    if (application == null)
      return Result.Fail<bool>("Такой заявки не существует");

    application.HiredExecutors ??= new HashSet<ProfileEntity>();
    application.InvitedExecutors ??= new HashSet<ProfileEntity>();
    application.SuggestedExecutors ??= new HashSet<ProfileEntity>();
    foreach (var worker in workers)
    {
      application.HiredExecutors.Add(worker);
      if (application.InvitedExecutors.Contains(worker))
        application.InvitedExecutors.Remove(worker);
      if (application.SuggestedExecutors.Contains(worker))
        application.SuggestedExecutors.Remove(worker);
    }
    await dataContext.SaveChangesAsync().ConfigureAwait(false);

    return Result.Ok(true);
  }

  private async Task<HashSet<ProfileEntity>> GetWorkersAsync(IEnumerable<Guid> workersIds)
  {
    HashSet<ProfileEntity> workers = new();
    foreach (var workerId in workersIds)
    {
      var worker = await dataContext.Profiles.FindAsync(workerId).ConfigureAwait(false);
      if (worker == null)
        continue;
      workers.Add(worker);
    }

    return workers;
  }
}
