using API.DAL;
using API.Modules.CompaniesModule.Entity;
using AutoMapper;

namespace API.Modules.CompaniesModule.MappingProfile;

public class CompanyMappingConverter :
  IValueConverter<Guid?, CompanyEntity>
{
  private readonly DataContext dataContext;

  public CompanyMappingConverter(DataContext dataContext)
  {
    this.dataContext = dataContext;
  }

  public CompanyEntity Convert(Guid? sourceMember, ResolutionContext context)
  {
    if (sourceMember == null)
      return null;

    var company = dataContext.Companies.Find(sourceMember);
    return company;
  }
}
