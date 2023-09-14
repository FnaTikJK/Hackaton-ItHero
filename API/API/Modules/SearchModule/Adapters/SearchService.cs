using API.DAL;
using API.Infrastructure;
using API.Infrastructure.Extensions;
using API.Modules.AccountsModule.Entity;
using API.Modules.ProfilesModule.DTO;
using API.Modules.SearchModule.DTO;
using API.Modules.SearchModule.Ports;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.SearchModule.Adapters;

public class SearchService : ISearchService
{
  private readonly DataContext dataContext;
  private readonly IMapper mapper;

  public SearchService(IMapper mapper, DataContext dataContext)
  {
    this.mapper = mapper;
    this.dataContext = dataContext;
  }

  public Result<SearchResponseDTO> Search(SearchRequestDTO searchRequest)
  {
    var res = dataContext.Profiles
      .Include(p => p.Company)
      .Include(p => p.Specializations)
      .Where(p => p.Account.Role == AccountRole.Executor);
    if (searchRequest.Name != null)
      res = res.Where(p => p.SecondName.Contains(searchRequest.Name));

    var profiles = mapper.Map<IEnumerable<ProfileOutDTO>>(res
      .Skip(searchRequest.Skip)
      .Take(searchRequest.Take)
      .ToList());
    return Result.Ok(new SearchResponseDTO
    {
      TotalCount = profiles.Count(),
      Profiles = profiles,
    });
  }
}
