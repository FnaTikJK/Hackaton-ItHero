using API.Modules.AccountsModule.Entity;
using API.Modules.AccountsModule.Ports;
using API.Modules.AccountsModule.Requests;
using AutoMapper;

namespace API.Modules.AccountsModule.Mapping
{
    public class AccountsMappingProfile : Profile
    {
        public AccountsMappingProfile()
        {
            CreateMap<RegisterRequest, LoginRequest>();
            CreateMap<RegisterRequest, Account>()
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.ConvertUsing<PasswordConverter, string>(src => src.Password));
        }

        private class PasswordConverter : IValueConverter<string, string>
        {
            private readonly IPasswordHasher passwordHasher;

            public PasswordConverter(IPasswordHasher passwordHasher)
            {
                this.passwordHasher = passwordHasher;
            }

            public string Convert(string notHashedPassword, ResolutionContext context)
            {
                return passwordHasher.CalculateHash(notHashedPassword);
            }
        }
    }
}
