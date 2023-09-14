using API.Infrastructure;
using API.Modules.SpecializationsModule.DTO;

namespace API.Modules.SpecializationsModule.Ports;

public interface ISpecializationsService
{
  public Result<IEnumerable<SpecializationOutDTO>> GetSpecializations();
}
