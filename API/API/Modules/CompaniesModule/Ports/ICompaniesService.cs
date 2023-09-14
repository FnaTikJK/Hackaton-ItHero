using API.Infrastructure;
using API.Modules.CompaniesModule.DTO;

namespace API.Modules.CompaniesModule.Ports
{
  public interface ICompaniesService
  {
    public Result<IEnumerable<CompanyOutDTO>> GetCompanies();
    public Task<Result<Guid>> CreateCompanyAsync(Guid ownerId, CompanyInnerDTO companyInner);
    public Task<Result<bool>> UpdateCompanyInfoAsync(Guid companyId, Guid userId, CompanyInnerDTO companyInner);
    public Task<Result<bool>> JoinCompanyAsync(Guid companyId, Guid userId);
    public Task<Result<bool>> LeaveCompanyAsync(Guid companyId, Guid userId);
    public Task<Result<bool>> ChangeOwner(Guid companyId, Guid oldOwnerId, Guid newOwnerId);
    public Task<Result<Guid>> GetCompanyIdByUserId(Guid userId);
    public Task<Result<bool>> IsUserIsOwner(Guid companyId, Guid userId);
  }
}
