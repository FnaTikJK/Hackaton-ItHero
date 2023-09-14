using API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.StaticsModule;

[Route("api/Statics/[controller]")]
[ApiController]
public class PhotosController : ControllerBase
{
  private const string PathToPhoto = "Modules/StaticsModule/Files/Photos";

  [HttpGet("My")]
  [Authorize]
  public async Task GetMyPhotoAsync()
  {
    await GetPhotoAsync(User.GetId());
  }

  [HttpPost("My"), DisableRequestSizeLimit]
  [Authorize]
  public async Task<ActionResult> UpdatePhoto()
  {
    var photo = Request.Form.Files.First();
    var fullPath = $"{PathToPhoto}/{User.GetId()}.jpg";
    await using var fileStream = new FileStream(fullPath, FileMode.Create);
    await photo.CopyToAsync(fileStream);
    return Ok();
  }

  [HttpGet("{id}")]
  public async Task GetPhotoAsync(Guid id)
  {
    HttpContext.Response.ContentType = "image/jpeg";
    await HttpContext.Response.SendFileAsync($"{PathToPhoto}/{id}.jpg");
  }
}
