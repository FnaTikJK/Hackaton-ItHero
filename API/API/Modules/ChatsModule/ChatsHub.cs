using API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.Modules.ChatsModule;

[Authorize]
public class ChatsHub : Hub
{
  private readonly ChatsService chatsService;

  public ChatsHub(ChatsService chatsService)
  {
    this.chatsService = chatsService;
  }

  public async Task Send(Guid chatId, string message)
  {
    var receiver = chatsService.SendMessage(chatId, Context.User.GetId(), message);
    Clients.User(receiver.ToString()).SendAsync("Recieve", message);
  }

  public async Task SendAll(Guid chatId, string message)
  {
    chatsService.SendMessage(chatId, Context.User.GetId(), message);
    Clients.All.SendAsync("Recieve", message);
  }
}

public class CustomUserIdProvider : IUserIdProvider
{
  public virtual string? GetUserId(HubConnectionContext connection)
  {
    return connection.User?.GetId().ToString();
  }
}
