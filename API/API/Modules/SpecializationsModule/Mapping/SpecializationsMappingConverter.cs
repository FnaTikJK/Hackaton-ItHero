using API.DAL;
using API.Modules.SpecializationsModule.DTO;
using API.Modules.SpecializationsModule.Entity;
using AutoMapper;

namespace API.Modules.SpecializationsModule.Mapping;

public class SpecializationsMappingConverter :
  IValueConverter<HashSet<Guid>?, HashSet<SpecializationEntity>?>,
  IValueConverter<HashSet<SpecializationEntity>?, HashSet<SpecializationOutDTO>?>
{
  private readonly DataContext dataContext;

  public SpecializationsMappingConverter(DataContext dataContext)
  {
    this.dataContext = dataContext;
  }

  public HashSet<SpecializationEntity>? Convert(HashSet<Guid>? sourceMember, ResolutionContext context)
  {
    if (sourceMember == null)
      return null;

    var result =  sourceMember
      .Select(guid => dataContext.Specializations.Find(guid))
      .Where(spec => spec != null)
      .Cast<SpecializationEntity>()
      .ToHashSet();
    return result.Count == 0
      ? null
      : result;
  }

  public HashSet<SpecializationOutDTO>? Convert(HashSet<SpecializationEntity>? sourceMember, ResolutionContext context)
  {
    if (sourceMember == null || sourceMember.Count == 0)
      return null;

    var result = sourceMember
      .Select(s => new SpecializationOutDTO() { Id = s.Id, Name = s.Name })
      .ToHashSet();
    return result.Count == 0
      ? null
      : result;
  }
}
