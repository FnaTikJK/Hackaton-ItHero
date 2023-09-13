using API.DAL;
using API.Infrastructure;
using API.Modules.ProfilesModule.DTO;
using API.Modules.ProfilesModule.Entity;
using API.Modules.ProfilesModule.Ports;
using AutoMapper;

namespace API.Modules.ProfilesModule.Adapters
{
    public class ProfilesService : IProfilesService
    {
        private readonly DataContext dataContexxt;
        private readonly IMapper mapper;

        public ProfilesService(IMapper mapper, DataContext dataContext)
        {
            this.mapper = mapper;
            this.dataContexxt = dataContext;
        }
        public async Task<Result<bool>> CreateProfileAsync(Guid profileId)
        {
            await dataContexxt.AddAsync(new ProfileEntity {Id = profileId});
            await dataContexxt.SaveChangesAsync();
            return Result.Ok(true);
        }

        public async Task<Result<ProfileOutDTO>> GetProfileAsync(Guid userId)
        {
            var cur = await dataContexxt.Profiles.FindAsync(userId);
            if (cur == null)
                return Result.Fail<ProfileOutDTO>("Такого пользователя не существует");

            return Result.Ok(mapper.Map<ProfileOutDTO>(cur));
        }

        public async Task<Result<bool>> CreateOrUpdateProfile(Guid id, ProfileInnerDTO profileInner)
        {
            var cur = await dataContexxt.Profiles.FindAsync(id);
            if (cur != null)
            {
                mapper.Map(profileInner, cur);
            }
            else
            {
                var profileEntity = mapper.Map<ProfileEntity>(profileInner);
                profileEntity.Id = id;
                await dataContexxt.Profiles.AddAsync(profileEntity);
            }

            await dataContexxt.SaveChangesAsync();
            return Result.Ok(true);
        }
    }
}
