using API.Modules.CompaniesModule.DTO;
using API.Modules.SpecializationsModule.DTO;

namespace API.Modules.ProfilesModule.DTO
{
    public class ProfileOutDTO
    {
        public Guid Id { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string ThirdName { get; set; }
        public string About { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public CompanyOutShortDTO? Company { get; set; }
        public HashSet<SpecializationOutDTO>? Specializations { get; set; }
  }
}
