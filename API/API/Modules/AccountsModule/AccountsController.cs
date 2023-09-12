using API.Modules.AccountsModule.Ports;
using API.Modules.AccountsModule.Requests;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Modules.AccountsModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
        {
            var response = await accountsService.RegisterAsync(registerRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Error);

            var principal = new ClaimsPrincipal(response.Value);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var response = await accountsService.LoginAsync(loginRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Error);

            var principal = new ClaimsPrincipal(response.Value);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return NoContent();
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<ActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }
    }
}
