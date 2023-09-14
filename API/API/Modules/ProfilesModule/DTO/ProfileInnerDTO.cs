using API.Modules.SpecializationsModule.Entity;

namespace API.Modules.ProfilesModule.DTO
{
    public class ProfileInnerDTO
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string ThirdName { get; set; }
        public string About { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid? CompanyId { get; set; }
        public HashSet<Guid>? Specializations { get; set; }
  }
}
