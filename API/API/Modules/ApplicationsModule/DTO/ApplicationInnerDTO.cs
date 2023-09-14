using System.ComponentModel.DataAnnotations;

namespace API.Modules.ApplicationsModule.DTO;

public class ApplicationInnerDTO
{
  public string? Title { get; set; }
  public decimal? Budget { get; set; }
  public long? CompletionTime { get; set; }
  public string? Text { get; set; }
}
