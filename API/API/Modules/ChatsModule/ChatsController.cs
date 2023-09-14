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
  public ActionResult CreateChat(CreateChatRequest createChatRequest)
  {
    return Ok(new { Id = chatsService.CreateChat(User.GetId(), createChatRequest.CompanionId) });
  }
  public class CreateChatRequest
  {
    public Guid CompanionId { get; set; }
  }

  [HttpPost("{chatId}")]
  [Authorize]
  public ActionResult SendMessage(Guid chatId, SendMessageRequest sendMessageRequest)
  {
    chatsService.SendMessage(chatId, User.GetId(), sendMessageRequest.Message);
    return NoContent();
  }

  public class SendMessageRequest
  {
    public string Message { get; set; }
  }
}
