
using API.Modules.ProfilesModule.Entity;

namespace API.Modules.ApplicationsModule.DTO;

public class ApplicationOutDTO
{
  public Guid Id { get; set; }
  public string? Title { get; set; }
  public decimal? Budget { get; set; }
  public long? CompletionTime { get; set; }
  public string? Text { get; set; }

  public HashSet<ProfileEntity> SuggestedExecutors { get; set; }
  public HashSet<ProfileEntity> InvitedExecutors { get; set; }
  public HashSet<ProfileEntity> HiredExecutors { get; set; }
}
