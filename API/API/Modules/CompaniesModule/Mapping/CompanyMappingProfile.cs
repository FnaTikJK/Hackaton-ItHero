using API.Modules.CompaniesModule.DTO;
using API.Modules.CompaniesModule.Entity;
using API.Modules.ProfilesModule.Entity;
using AutoMapper;

namespace API.Modules.CompaniesModule.MappingProfile
{
  public class CompanyMappingProfile : Profile
  {
    public CompanyMappingProfile()
    {
      CreateMap<CompanyInnerDTO, CompanyEntity>();
      CreateMap<CompanyEntity, CompanyOutDTO>()
        .ForMember(dest => dest.Workers, opt => opt.MapFrom(src => src.Workers));
      CreateMap<CompanyEntity, CompanyOutShortDTO>();
    }
  }
}
