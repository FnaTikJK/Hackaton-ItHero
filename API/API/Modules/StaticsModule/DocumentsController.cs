using API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.StaticsModule;

[Route("api/Statics/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
  private const string PathToDocuments = "Modules/StaticsModule/Files/Photos";
  private const string Spark = "Spark";
  private const string Registration = "Registration";
  private const string Egrul = "Egrul";

  [HttpGet("My")]
  [Authorize]
  public async Task GetMyDocuments()
  {
    await GetDocumentsAsync(User.GetId());
  }

  [HttpGet("{id}")]
  public async Task GetDocumentsAsync(Guid id)
  {
    HttpContext.Response.ContentType = ".doc/.docx";

    await HttpContext.Response.SendFileAsync($"{PathToDocuments}/{Spark}__{id}.doc");
    await HttpContext.Response.SendFileAsync($"{PathToDocuments}/{Registration}__{id}.doc");
    await HttpContext.Response.SendFileAsync($"{PathToDocuments}/{Egrul}__{id}.doc");
  }

  [HttpPost("My")]
  [Authorize]
  public async Task<ActionResult> UpdatePhoto()
  {
    var id = User.GetId();
    var files = Request.Form.Files;
    foreach (var file in files)
    {
      var path = file.Name switch
      {
        $"{Spark}.doc" => $"{PathToDocuments}.{Spark}__{id}.doc",
        $"{Registration}.doc" => $"{PathToDocuments}.{Spark}__{id}.doc",
        $"{Egrul}.doc" => $"{PathToDocuments}.{Spark}__{id}.doc",
        _ => throw new ArgumentException("Некорректное название файла")
      };
      await using var fileStream = new FileStream(path, FileMode.Create);
      await file.CopyToAsync(fileStream);
    }
    return Ok();
  }
}
