using API.Infrastructure;
using API.Modules.ProfilesModule.Adapters;
using API.Modules.ProfilesModule.Mapping;
using API.Modules.ProfilesModule.Ports;

namespace API.Modules.ProfilesModule
{
    public class ProfilesModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IProfilesService, ProfilesService>();
            services.AddAutoMapper(typeof(ProfilesMappingProfile));

            return services;
        }

        public void ConfigureHubs(WebApplication app)
        {
            return;
        }
    }
}
