using API.Infrastructure;
using System.Security.Claims;
using API.Modules.AccountsModule.Requests;

namespace API.Modules.AccountsModule.Ports
{
    public interface IAccountsService
    {
        Task<Result<ClaimsIdentity>> RegisterAsync(RegisterRequest registerRequest);
        Task<Result<ClaimsIdentity>> LoginAsync(LoginRequest loginRequest);
    }
}
