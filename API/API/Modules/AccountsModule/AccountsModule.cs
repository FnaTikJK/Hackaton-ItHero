using API.Infrastructure;
using API.Modules.AccountsModule.Adapters;
using API.Modules.AccountsModule.Mapping;
using API.Modules.AccountsModule.Ports;

namespace API.Modules.AccountsModule
{
    public class AccountsModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddAutoMapper(typeof(AccountsMappingProfile));

            return services;
        }

        public void ConfigureHubs(WebApplication app)
        {
            return;
        }
    }
}
