using API.DAL;
using API.Infrastructure;
using API.Modules.SpecializationsModule.DTO;
using API.Modules.SpecializationsModule.Ports;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.SpecializationsModule.Adapters;

public class SpecializationsService : ISpecializationsService
{
  private readonly DataContext dataContext;
  private readonly IMapper mapper;

  public SpecializationsService(IMapper mapper, DataContext dataContext)
  {
    this.mapper = mapper;
    this.dataContext = dataContext;
  }

  public Result<IEnumerable<SpecializationOutDTO>> GetSpecializations()
  {
    return Result.Ok(mapper.Map<IEnumerable<SpecializationOutDTO>>(dataContext.Specializations));
  }
}
