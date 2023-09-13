using API.Infrastructure;
using API.Modules.CompaniesModule.Adapters;
using API.Modules.CompaniesModule.MappingProfile;
using API.Modules.CompaniesModule.Ports;

namespace API.Modules.CompaniesModule
{
  public class CompaniesModule : IModule
  {
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
      services.AddScoped<ICompaniesService, CompaniesService>();
      services.AddAutoMapper(typeof(CompanyMappingProfile));

      return services;
    }

    public void ConfigureHubs(WebApplication app)
    {
      return;
    }
  }
}
