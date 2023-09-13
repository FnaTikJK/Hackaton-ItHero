using API.Modules.ProfilesModule.DTO;
using API.Modules.ProfilesModule.Entity;
using AutoMapper;

namespace API.Modules.ProfilesModule.Mapping
{
    public class ProfilesMappingProfile : Profile
    {
        public ProfilesMappingProfile()
        {
            CreateMap<ProfileEntity, ProfileOutDTO>();
            CreateMap<ProfileInnerDTO, ProfileEntity>();
        }
    }
}
