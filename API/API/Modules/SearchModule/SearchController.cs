using API.Modules.SearchModule.DTO;
using API.Modules.SearchModule.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.SearchModule;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
  private readonly ISearchService searchService;

  public SearchController(ISearchService searchService)
  {
    this.searchService = searchService;
  }

  [HttpGet]
  public ActionResult<SearchResponseDTO> Search([FromQuery]SearchRequestDTO searchRequest)
  {
    var response = searchService.Search(searchRequest);

    return response.IsSuccess
      ? Ok(response.Value)
      : BadRequest(response.Error);
  }
}
