using API.Infrastructure;
using System.Security.Claims;
using API.Modules.AccountsModule.Entity;
using API.Modules.AccountsModule.Requests;

namespace API.Modules.AccountsModule.Ports
{
    public interface IAccountsService
    {
        Task<Result<(ClaimsIdentity credentials, AccountRole role)>> RegisterAsync(RegisterRequest registerRequest);
        Task<Result<(ClaimsIdentity credentials, AccountRole role)>> LoginAsync(LoginRequest loginRequest);
    }
}
