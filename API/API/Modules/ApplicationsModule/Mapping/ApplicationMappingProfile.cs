using API.Modules.ApplicationsModule.DTO;
using API.Modules.ApplicationsModule.Entity;
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
