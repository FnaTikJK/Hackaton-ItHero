using System.Collections;
using API.DAL;
using API.Infrastructure;
using API.Modules.ApplicationsModule.DTO;
using API.Modules.ApplicationsModule.Entity;
using API.Modules.ApplicationsModule.Ports;
using API.Modules.ProfilesModule.Entity;
using API.Modules.SpecializationsModule.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Modules.ApplicationsModule.Adapters;

public class  ApplicationsService : IApplicationsService
{
  private readonly DataContext dataContext;
  private readonly IMapper mapper;

  private IIncludableQueryable<ApplicationEntity, HashSet<SpecializationEntity>?> Applications => dataContext.Applications
    .Include(a => a.InvitedExecutors)
    .ThenInclude(p => p.Company)
    .Include(a => a.InvitedExecutors)
    .ThenInclude(p => p.Specializations)

    .Include(a => a.HiredExecutors)
    .ThenInclude(p => p.Company)
    .Include(a => a.HiredExecutors)
    .ThenInclude(p => p.Specializations)

    .Include(a => a.SuggestedExecutors)
    .ThenInclude(p => p.Company)
    .Include(a => a.SuggestedExecutors)
    .ThenInclude(p => p.Specializations);

  public ApplicationsService(DataContext dataContext, IMapper mapper)
  {
    this.dataContext = dataContext;
    this.mapper = mapper;
  }
  public Result<IEnumerable<ApplicationOutDTO>> GetApplicationsAsync(Guid ownerId)
  {
    var applications = Applications.Where(app => app.OwnerId == ownerId);
    var y = dataContext.Applications.ToList();
    var x = applications.ToList();
    return Result.Ok(mapper.Map<IEnumerable<ApplicationOutDTO>>(applications));
  }

  public async Task<Result<Guid>> CreateApplicationAsync(Guid ownerId, ApplicationInnerDTO applicationInner)
  {
    var application = mapper.Map<ApplicationEntity>(applicationInner);
    application.ExpiryAt = DateTime.UtcNow + TimeSpan.FromTicks(applicationInner.CompletionTime ?? 0);
    application.OwnerId = ownerId;
    await dataContext.Applications.AddAsync(application);
    await dataContext.SaveChangesAsync();
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

  public async Task RemoveWorker(Guid applicationId, Guid workerId)
  {
    var application = await Applications.FirstOrDefaultAsync(a => a.Id == applicationId);
    if (application == null)
      throw new Exception("Нет заявки");

    application.InvitedExecutors?.RemoveWhere(w => w.Id == workerId);
    application.SuggestedExecutors?.RemoveWhere(w => w.Id == workerId);
    application.HiredExecutors?.RemoveWhere(w => w.Id == workerId);
    await dataContext.SaveChangesAsync();
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
