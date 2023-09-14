using API.Infrastructure;
using API.Modules.ApplicationsModule.Adapters;
using API.Modules.ApplicationsModule.Mapping;
using API.Modules.ApplicationsModule.Ports;

namespace API.Modules.ApplicationsModule;

public class ApplicationsModule : IModule
{
  public IServiceCollection RegisterModule(IServiceCollection services)
  {
    services.AddScoped<IApplicationsService, ApplicationsService>();
    services.AddAutoMapper(typeof(ApplicationMappingProfile));

    return services;
  }

  public void ConfigureHubs(WebApplication app)
  {
    return;
  }
}
