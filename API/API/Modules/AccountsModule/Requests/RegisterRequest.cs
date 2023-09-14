using API.Modules.AccountsModule.Entity;
using System.ComponentModel.DataAnnotations;

namespace API.Modules.AccountsModule.Requests
{
    public class RegisterRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public AccountRole Role { get; set; }
    }
}
