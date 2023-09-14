namespace API.Modules.SearchModule.DTO;

public class SearchRequestDTO
{
  public int Skip { get; set; } = 0;
  public int Take { get; set; } = 10;
  public string? Name { get; set; }
}
