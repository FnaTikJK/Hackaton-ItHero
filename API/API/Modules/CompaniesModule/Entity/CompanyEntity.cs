using System.ComponentModel.DataAnnotations;
using API.Modules.ProfilesModule.Entity;

namespace API.Modules.CompaniesModule.Entity
{
  public class CompanyEntity
  {
    [Key]
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public int Inn { get; set; }
    public int Kpp { get; set; }
    public string About { get; set; }
    public HashSet<ProfileEntity> Workers { get; set; }
  }
}
