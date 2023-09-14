using API.Infrastructure;
using API.Modules.SearchModule.DTO;

namespace API.Modules.SearchModule.Ports;

public interface ISearchService
{
  Result<SearchResponseDTO> Search(SearchRequestDTO searchRequest);
}
