using API.DAL;
using API.Modules.ChatsModule.DTO;
using API.Modules.ChatsModule.Entity;
using API.Modules.ProfilesModule.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.ChatsModule;

public class ChatsService
{
  private readonly DataContext dataContext;
  private readonly IMapper mapper;

  public ChatsService(IMapper mapper, DataContext dataContext)
  {
    this.mapper = mapper;
    this.dataContext = dataContext;
  }

  public IEnumerable<ChatDTO> GetChats(Guid userId)
  {
    var chats = dataContext.Chats
      .Include(c => c.Messages)
      .Include(c => c.Users)
      .Where(c => c.Users.Any(u => u.Id == userId))
      .ToList();

    return mapper.Map<IEnumerable<ChatDTO>>(chats,
      opt => opt.Items["userId"] = userId);
  }

  public (IEnumerable<ChatMessageDTO> sent, IEnumerable<ChatMessageDTO> received) GetMessagesInChat(Guid chatId, Guid curUserId)
  {
    var messages = dataContext.Messages
      .Where(m => m.Chat.Id == chatId);

    var sent = messages.Where(m => m.Sender.Id == curUserId).ToList();
    var received = messages.Where(m => m.Sender.Id != curUserId).ToList();

    var setMapped = mapper.Map<IEnumerable<ChatMessageDTO>>(sent);
     return (sent: mapper.Map<IEnumerable<ChatMessageDTO>>(sent),
      received: mapper.Map<IEnumerable<ChatMessageDTO>>(received));
  }

  public Guid CreateChat(Guid firstUserId, Guid secondUserId)
  {
    var cur = dataContext.Chats
      .Any(c =>
        c.Users.Any(u => u.Id == firstUserId)
        && c.Users.Any(u => u.Id == secondUserId));
    if (cur)
      throw new Exception("Chat already exists");

    var firstUser = dataContext.Profiles.Find(firstUserId);
    var secondUser = dataContext.Profiles.Find(secondUserId);
    var chat = new ChatEntity()
    {
      Users = new HashSet<ProfileEntity>() { firstUser, secondUser },
    };
    dataContext.Chats.Add(chat);
    dataContext.SaveChanges();
    return chat.Id;
  }

  public Guid SendMessage(Guid chatId, Guid SenderId, string message)
  {
    var chat = dataContext.Chats
      .Include(c => c.Users)
      .First(c => c.Id == chatId);
    var sender = dataContext.Profiles.Find(SenderId);
    var receiver = chat.Users.First(u => u.Id != SenderId);

    dataContext.Add(new MessageEntity()
    {
      Chat = chat,
      Sender = sender,
      Message = message,
      Date = DateTime.Now,
    });
    dataContext.SaveChanges();
    return receiver.Id;
  }
}
