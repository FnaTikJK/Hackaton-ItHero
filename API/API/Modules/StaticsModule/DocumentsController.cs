using API.Infrastructure;
using API.Modules.CompaniesModule.Ports;
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
  private readonly ICompaniesService companiesService;

  public DocumentsController(ICompaniesService companiesService)
  {
    this.companiesService = companiesService;
  }

  [HttpGet("My")]
  [Authorize]
  public async Task<ActionResult> GetMyDocuments()
  {
    var id = User.GetId();
    var compRes = await companiesService.GetCompanyIdByUserId(id);
    if (!compRes.IsSuccess)
      return BadRequest(compRes.Error);

    await GetDocumentsAsync(compRes.Value);
    return NoContent();
  }

  [HttpGet("{companyId}")]
  public async Task GetDocumentsAsync(Guid companyId)
  {
    HttpContext.Response.ContentType = ".doc/.docx";

    await HttpContext.Response.SendFileAsync($"{PathToDocuments}/{Spark}__{companyId}.doc");
    await HttpContext.Response.SendFileAsync($"{PathToDocuments}/{Registration}__{companyId}.doc");
    await HttpContext.Response.SendFileAsync($"{PathToDocuments}/{Egrul}__{companyId}.doc");
  }

  [HttpPost("My")]
  [Authorize]
  public async Task<ActionResult> UpdateFilesAsync()
  {
    var id = User.GetId();
    var compRes = await companiesService.GetCompanyIdByUserId(id);
    if (!compRes.IsSuccess)
      return BadRequest("Вы не состоите в компании");
    var isOwner = await companiesService.IsUserIsOwner(compRes.Value, id);
    if (!isOwner.IsSuccess)
      return BadRequest(isOwner.Error);

    var companyId = compRes.Value;
    var files = Request.Form.Files;
    foreach (var file in files)
    {
      if (file.ContentType != "application/msword")
        return BadRequest("Only .doc allowed");
      var path = file.Name switch
      {
        $"{Spark}" => $"{PathToDocuments}/{Spark}{companyId}.doc",
        $"{Registration}" => $"{PathToDocuments}/{Registration}{companyId}.doc",
        $"{Egrul}" => $"{PathToDocuments}/{Egrul}__{companyId}.doc",
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
