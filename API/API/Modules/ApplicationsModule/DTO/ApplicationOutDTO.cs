
using API.Modules.ProfilesModule.DTO;
using API.Modules.ProfilesModule.Entity;

namespace API.Modules.ApplicationsModule.DTO;

public class ApplicationOutDTO
{
  public Guid Id { get; set; }
  public string? Title { get; set; }
  public decimal? Budget { get; set; }
  public string? Text { get; set; }

  public HashSet<ProfileOutDTO>? SuggestedExecutors { get; set; }
  public HashSet<ProfileOutDTO>? InvitedExecutors { get; set; }
  public HashSet<ProfileOutDTO>? HiredExecutors { get; set; }
}
