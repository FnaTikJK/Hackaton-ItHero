using API.DAL;
using API.Infrastructure;
using API.Modules.CompaniesModule.DTO;
using API.Modules.CompaniesModule.Entity;
using API.Modules.CompaniesModule.Ports;
using API.Modules.ProfilesModule.Entity;
using AutoMapper;

namespace API.Modules.CompaniesModule.Adapters
{
  public class CompaniesService : ICompaniesService
  {
    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public CompaniesService(IMapper mapper, DataContext dataContext)
    {
      this.mapper = mapper;
      this.dataContext = dataContext;
    }
    public Result<IEnumerable<CompanyOutDTO>> GetCompanies()
    {
      var companies = dataContext.Companies.ToList();

      return Result.Ok(mapper.Map<IEnumerable<CompanyOutDTO>>(companies));
    }

    public async Task<Result<Guid>> CreateCompanyAsync(Guid ownerId, CompanyInnerDTO companyInner)
    {
      var company = mapper.Map<CompanyEntity>(companyInner);
      var profile = await dataContext.Profiles.FindAsync(ownerId);
      if (profile == null)
        return Result.Fail<Guid>("Такого пользователя не сущесвтует");
      company.OwnerId = ownerId;
      company.Workers = new HashSet<ProfileEntity>() {profile};

      await dataContext.Companies.AddAsync(company);
      await dataContext.SaveChangesAsync();

      return Result.Ok(company.Id);
    }

    public async Task<Result<bool>> UpdateCompanyInfoAsync(Guid companyId, Guid userId, CompanyInnerDTO companyInner)
    {
      var cur = await dataContext.Companies.FindAsync(companyId);
      if (cur == null)
        return Result.Fail<bool>("Такой компании не существует");

      if (cur.OwnerId != userId)
        return Result.Fail<bool>("У вас нет прав");

      mapper.Map(companyInner, cur);
      await dataContext.SaveChangesAsync();

      return Result.Ok(true);
    }

    public async Task<Result<bool>> JoinCompanyAsync(Guid companyId, Guid userId)
    {
      var cur = await dataContext.Companies.FindAsync(companyId);
      if (cur == null)
        return Result.Fail<bool>("Такой компании не существует");

      var profile = await dataContext.Profiles.FindAsync(userId);
      if (profile == null)
        return Result.Fail<bool>("Такого пользователя не существует");

      if (cur.Workers == null)
        cur.Workers = new HashSet<ProfileEntity>();
      cur.Workers.Add(profile);
      await dataContext.SaveChangesAsync();

      return Result.Ok(true);
    }

    public async Task<Result<bool>> LeaveCompanyAsync(Guid companyId, Guid userId)
    {
      var cur = await dataContext.Companies.FindAsync(companyId);
      if (cur == null)
        return Result.Fail<bool>("Такой компании не существует");

      var profile = await dataContext.Profiles.FindAsync(userId);
      if (profile == null)
        return Result.Fail<bool>("Такого пользователя не существует");

      cur.Workers.Remove(profile);
      if (cur.Workers.Count != 0 && cur.OwnerId == userId)
        return Result.Fail<bool>("Администратор компании не может покинуть её. Сперва передайте права");

      if (cur.Workers.Count == 0)
        dataContext.Companies.Remove(cur);
      await dataContext.SaveChangesAsync();

      return Result.Ok(true);
    }

    public async Task<Result<bool>> ChangeOwner(Guid companyId, Guid oldOwnerId, Guid newOwnerId)
    {
      var cur = await dataContext.Companies.FindAsync(companyId);
      if (cur == null)
        return Result.Fail<bool>("Такой компании не существует");

      if (cur.OwnerId != oldOwnerId)
        return Result.Fail<bool>("Недостаточно прав");

      var newOwnerProfile = await dataContext.Profiles.FindAsync(newOwnerId);
      if (newOwnerProfile == null)
        return Result.Fail<bool>("Того, кому вы хотите передать права не существует");

      cur.OwnerId = newOwnerId;
      await dataContext.SaveChangesAsync();
      return Result.Ok(true);
    }
  }
}
