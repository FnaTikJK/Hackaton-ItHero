using API.Modules.CompaniesModule.Entity;
using API.Modules.CompaniesModule.MappingProfile;
using API.Modules.ProfilesModule.DTO;
using API.Modules.ProfilesModule.Entity;
using API.Modules.SpecializationsModule.Entity;
using API.Modules.SpecializationsModule.Mapping;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace API.Modules.ProfilesModule.Mapping
{
    public class ProfilesMappingProfile : Profile
    {
        public ProfilesMappingProfile()
        {
          CreateMap<ProfileInnerDTO, ProfileEntity>()
            .ForMember(dest => dest.Company, opt
              => opt.ConvertUsing<CompanyMappingConverter, Guid?>(src => src.CompanyId))
            .ForMember(dest => dest.Specializations, opt
              => opt.ConvertUsing<SpecializationsMappingConverter, HashSet<Guid>?>(src => src.Specializations));

          CreateMap<ProfileEntity, ProfileOutDTO>()
            .ForMember(dest => dest.Specializations, opt
              => opt.ConvertUsing<SpecializationsMappingConverter, HashSet<SpecializationEntity>?>(src => src.Specializations))
            .ForMember(dest => dest.Company, opt
              => opt.MapFrom(src => src.Company));
        }
    }
}
