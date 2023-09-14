using API.Modules.ChatsModule.DTO;
using API.Modules.ChatsModule.Entity;
using API.Modules.ProfilesModule.DTO;
using API.Modules.ProfilesModule.Entity;
using AutoMapper;

namespace API.Modules.ChatsModule.MappingProfile;

public class ChatsMappingProfile : Profile
{
  public ChatsMappingProfile()
  {
    CreateMap<MessageEntity, ChatMessageDTO>();
    CreateMap<ChatEntity, ChatDTO>()
      .ForMember(dest => dest.Companion, opt
        => opt.ConvertUsing<MessagesConverter, HashSet<ProfileEntity>>(src => src.Users))
      .ForMember(dest => dest.LastMessage, opt
        => opt.MapFrom(src => getLastMessage(src.Messages)));
  }

  private string getLastMessage(HashSet<MessageEntity>? messages)
  {
    return messages == null || messages.Count == 0
      ? ""
      : messages.OrderBy(m => m.Date).Last().Message;
  }

  private class MessagesConverter : IValueConverter<HashSet<ProfileEntity>, ProfileOutDTO>
  {
    public ProfileOutDTO Convert(HashSet<ProfileEntity> sourceMember, ResolutionContext context)
    {
      var userGuid = Guid.Parse(context.Items["userId"].ToString());
      var profileEntity = sourceMember.First(s => s.Id != userGuid);
      return new ProfileOutDTO
      {
        Id = profileEntity.Id,
        SecondName = profileEntity.SecondName,
        FirstName = profileEntity.FirstName,
        ThirdName = profileEntity.ThirdName ?? "",
      };
    }
  }
}
