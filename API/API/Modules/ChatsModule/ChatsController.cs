using API.Infrastructure;
using API.Modules.ChatsModule.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.ChatsModule;

[Route("api/[controller]")]
[ApiController]
public class ChatsController : ControllerBase
{
  private readonly ChatsService chatsService;

  public ChatsController(ChatsService chatsService)
  {
    this.chatsService = chatsService;
  }

  [HttpGet]
  [Authorize]
  public ActionResult<IEnumerable<ChatDTO>> GetChats()
  {
    return Ok(chatsService.GetChats(User.GetId()));
  }

  [HttpGet("{chatId}")]
  [Authorize]
  public ActionResult<(IEnumerable<ChatMessageDTO> sent, IEnumerable<ChatMessageDTO> received)> GetMessages(Guid chatId)
  {
    var (sent, received) = chatsService.GetMessagesInChat(chatId, User.GetId());
    return Ok(new {Sent = sent, Received = received });
  }

  [HttpPost]
  [Authorize]
  public ActionResult CreateChat(Guid companionId)
  {
    return Ok(new { Id = chatsService.CreateChat(User.GetId(), companionId) });
  }

  [HttpPost("{chatId}")]
  [Authorize]
  public ActionResult SendMessage(Guid chatId, string message)
  {
    chatsService.SendMessage(chatId, User.GetId(), message);
    return NoContent();
  }
}
