using API.Modules.ProfilesModule.Entity;

namespace API.Modules.ChatsModule.Entity;

public class ChatEntity
{
  public Guid Id { get; set; }
  public HashSet<ProfileEntity> Users { get; set; }
  public HashSet<MessageEntity>? Messages { get; set; }
}
