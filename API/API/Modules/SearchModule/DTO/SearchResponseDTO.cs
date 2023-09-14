using API.Modules.ProfilesModule.DTO;

namespace API.Modules.SearchModule.DTO;

public class SearchResponseDTO
{
  public int TotalCount { get; set; }
  public IEnumerable<ProfileOutDTO> Profiles { get; set; }
}
