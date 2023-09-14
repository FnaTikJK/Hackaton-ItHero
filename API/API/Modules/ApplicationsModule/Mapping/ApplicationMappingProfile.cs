using API.Modules.ApplicationsModule.DTO;
using API.Modules.ApplicationsModule.Entity;
using API.Modules.ProfilesModule.DTO;
using API.Modules.ProfilesModule.Entity;
using API.Modules.SpecializationsModule.DTO;
using API.Modules.SpecializationsModule.Mapping;
using AutoMapper;

namespace API.Modules.ApplicationsModule.Mapping;

public class ApplicationMappingProfile : Profile
{
  public ApplicationMappingProfile()
  {
    CreateMap<ApplicationInnerDTO, ApplicationEntity>();
    CreateMap<ApplicationEntity, ApplicationOutDTO>();
  }
}
