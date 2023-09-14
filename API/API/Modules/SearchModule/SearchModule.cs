using API.Infrastructure;
using API.Modules.SearchModule.Adapters;
using API.Modules.SearchModule.Ports;

namespace API.Modules.SearchModule;

public class SearchModule : IModule
{
  public IServiceCollection RegisterModule(IServiceCollection services)
  {
    services.AddScoped<ISearchService, SearchService>();

    return services;
  }

  public void ConfigureHubs(WebApplication app)
  {
    return;
  }
}
