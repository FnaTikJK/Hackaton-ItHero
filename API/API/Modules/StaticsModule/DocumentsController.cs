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
  public async Task<ActionResult> UpdateFilesAsync()
  {
    var id = User.GetId();
    var files = Request.Form.Files;
    foreach (var file in files)
    {
      if (file.ContentType != "application/msword")
        return BadRequest("Only .doc allowed");
      var path = file.Name switch
      {
        $"{Spark}" => $"{PathToDocuments}/{Spark}{id}.doc",
        $"{Registration}" => $"{PathToDocuments}/{Registration}{id}.doc",
        $"{Egrul}" => $"{PathToDocuments}/{Egrul}__{id}.doc",
        _ => ""
      };
      if (path == "")
        return BadRequest("Неправильное имя файла(должен быть с .doc)");

      await using var fileStream = new FileStream(path, FileMode.Create);
      await file.CopyToAsync(fileStream);
    }
    return NoContent();
  }
}
