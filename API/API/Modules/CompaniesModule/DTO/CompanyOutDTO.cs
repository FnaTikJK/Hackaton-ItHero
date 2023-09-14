using API.Modules.ProfilesModule.DTO;

namespace API.Modules.CompaniesModule.DTO
{
  public class CompanyOutDTO
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Inn { get; set; }
    public int Kpp { get; set; }
    public string About { get; set; }
    public HashSet<ProfileOutDTO> Workers { get; set; }
  }
}
