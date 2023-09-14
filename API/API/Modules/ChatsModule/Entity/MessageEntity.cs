using System.ComponentModel.DataAnnotations;
using API.Modules.ProfilesModule.Entity;

namespace API.Modules.ChatsModule.Entity;

public class MessageEntity
{
  public Guid Id { get; set; }
  public ChatEntity Chat { get; set; }
  public ProfileEntity Sender { get; set; }
  public string Message { get; set; }
  public DateTime Date { get; set; }
}
