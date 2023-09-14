using API.Infrastructure;
using API.Modules.SpecializationsModule.Adapters;
using API.Modules.SpecializationsModule.Ports;

namespace API.Modules.SpecializationsModule;

public class SpecializationsModule : IModule
{
  public IServiceCollection RegisterModule(IServiceCollection services)
  {
    services.AddScoped<ISpecializationsService, SpecializationsService>();

    return services;
  }

  public void ConfigureHubs(WebApplication app)
  {
    return;
  }
}
