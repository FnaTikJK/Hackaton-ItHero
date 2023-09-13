using API.Infrastructure;
using API.Modules.ProfilesModule.DTO;
using API.Modules.ProfilesModule.Ports;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.ProfilesModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfilesService profilesService;

        public ProfilesController(IProfilesService profilesService)
        {
            this.profilesService = profilesService;
        }

        [HttpGet("My")]
        [Authorize]
        public async Task<ActionResult<ProfileOutDTO>> GetMyProfile()
        {
            var response = await profilesService.GetProfileAsync(User.GetId());

            return response.IsSuccess ? Ok(response.Value) 
                : BadRequest(response.Error);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileOutDTO>> GetProfileAsync(Guid id)
        {
            var response = await profilesService.GetProfileAsync(id);

            return response.IsSuccess
                ? Ok(response.Value)
                : BadRequest(response.Error);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateOrUpdateProfileAsync(ProfileInnerDTO profileInner)
        {
            var response = await profilesService.CreateOrUpdateProfile(User.GetId(), profileInner);

            return response.IsSuccess ? NoContent()
                : BadRequest(response.Error);
        }
    }
}
