using System.ComponentModel.DataAnnotations;
using API.Modules.ProfilesModule.Entity;

namespace API.Modules.SpecializationsModule.Entity;

public class SpecializationEntity
{
  [Key]
  public Guid Id { get; set; }
  public string Name { get; set; }
  public HashSet<ProfileEntity>? Profiles { get; set; }
}
