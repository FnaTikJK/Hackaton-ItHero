using API.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Modules.ChatsModule;

public class ChatsModule : IModule
{
  public IServiceCollection RegisterModule(IServiceCollection services)
  {
    services.AddScoped<ChatsService>();

    return services;
  }

  public void ConfigureHubs(WebApplication app)
  {
    app.MapHub<ChatsHub>("/api/Chats");
  }
}
