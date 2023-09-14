using API.Modules.SpecializationsModule.DTO;
using API.Modules.SpecializationsModule.Entity;
using AutoMapper;

namespace API.Modules.SpecializationsModule.Mapping;

public class SpecializationsMappingProfile : Profile
{
  public SpecializationsMappingProfile()
  {
    CreateMap<SpecializationInnerDTO, SpecializationEntity>();
    CreateMap<SpecializationEntity, SpecializationOutDTO>();
  }
}
