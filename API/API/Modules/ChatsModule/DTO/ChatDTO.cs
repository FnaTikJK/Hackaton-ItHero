using API.Modules.ProfilesModule.DTO;

namespace API.Modules.ChatsModule.DTO;

public class ChatDTO
{
  public Guid Id { get; set; }
  public ProfileOutDTO Companion { get; set; }
  public string LastMessage { get; set; }
}
