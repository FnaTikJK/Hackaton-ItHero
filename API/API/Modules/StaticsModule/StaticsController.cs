using API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.StaticsModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticsController : ControllerBase
    {
        private const string PathToPhoto = "Modules/StaticsModule/Files/Photos";

        [HttpGet("Photo/My")]
        [Authorize]
        public async Task GetMyPhotoAsync()
        {
            await GetPhotoAsync(User.GetId());
        }

        [HttpPost("Photo/My")]
        [Authorize]
        public async Task<ActionResult> UpdatePhoto()
        {
            var photo = Request.Form.Files.First();
            var fullPath = $"{PathToPhoto}/{User.GetId()}.jpg";
            await using var fileStream = new FileStream(fullPath, FileMode.Create);
            await photo.CopyToAsync(fileStream);
            return Ok();
        }

        [HttpGet("Photo/{id}")]
        public async Task GetPhotoAsync(Guid id)
        {
            HttpContext.Response.ContentType = "image/jpeg";
            await HttpContext.Response.SendFileAsync($"{PathToPhoto}/{id}.jpg");
        }
    }
}
