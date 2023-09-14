using System.ComponentModel.DataAnnotations;
using API.Modules.AccountsModule.Entity;
using API.Modules.CompaniesModule.Entity;
using API.Modules.SpecializationsModule.Entity;

namespace API.Modules.ProfilesModule.Entity
{
    public class ProfileEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string? ThirdName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AccountEntity Account { get; set; }
        public CompanyEntity? Company { get; set; }
        public HashSet<SpecializationEntity>? Specializations { get; set; }
    }
}
