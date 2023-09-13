using System.ComponentModel.DataAnnotations;

namespace API.Modules.ProfilesModule.Entity
{
    public class ProfileEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string? ThirdName { get; set; }
        public int? Inn { get; set; }
        public int? Kpp { get; set; }
    }
}
