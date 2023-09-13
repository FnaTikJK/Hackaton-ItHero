using API.Infrastructure;
using API.Modules.ProfilesModule.DTO;

namespace API.Modules.ProfilesModule.Ports
{
    public interface IProfilesService
    {
        Task<Result<bool>> CreateProfileAsync(Guid profileId);
        Task<Result<ProfileOutDTO>> GetProfileAsync(Guid userId);
        Task<Result<bool>> CreateOrUpdateProfile(Guid id, ProfileInnerDTO profileInner);
    }
}
