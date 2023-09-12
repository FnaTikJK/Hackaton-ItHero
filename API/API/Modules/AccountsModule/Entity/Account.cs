using System.ComponentModel.DataAnnotations;

namespace API.Modules.AccountsModule.Entity
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public AccountRole Role { get; set; }
    }
}
