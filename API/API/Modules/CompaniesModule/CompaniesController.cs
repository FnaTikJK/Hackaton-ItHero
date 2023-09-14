using API.Infrastructure;
using API.Modules.CompaniesModule.DTO;
using API.Modules.CompaniesModule.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.CompaniesModule
{
  [Route("api/[controller]")]
  [ApiController]
  public class CompaniesController : ControllerBase
  {
    private readonly ICompaniesService companiesService;

    public CompaniesController(ICompaniesService companiesService)
    {
      this.companiesService = companiesService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CompanyOutDTO>> GetCompanies()
    {
      var response = companiesService.GetCompanies();

      return response.IsSuccess ? Ok(response.Value)
        : BadRequest(response.Error);
    }

    [HttpGet("My")]
    [Authorize]
    public async Task<ActionResult<CompanyOutDTO>> GetMyCompany()
    {
      var response = await companiesService.GetCompanyIdByUserId(User.GetId());
      if (!response.IsSuccess)
        return BadRequest(response.Error);

      var company = companiesService.GetCompany(response.Value);
      return Ok(company.Value);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> CreateCompanyAsync(CompanyInnerDTO companyInner)
    {
      var response = await companiesService.CreateCompanyAsync(User.GetId(), companyInner);

      return response.IsSuccess ? Ok(new { id = response.Value })
        : BadRequest(response.Error);
    }

    [HttpPut("{companyId}")]
    [Authorize]
    public async Task<ActionResult> UpdateCompanyAsync(Guid companyId, CompanyInnerDTO companyInner)
    {
      var response = await companiesService.UpdateCompanyInfoAsync(companyId, User.GetId(), companyInner);

      return response.IsSuccess ? NoContent()
        : BadRequest(response.Error);
    }

    [HttpPost("{companyId}/Join")]
    [Authorize]
    public async Task<ActionResult> JoinCompanyAsync(Guid companyId)
    {
      var response = await companiesService.JoinCompanyAsync(companyId, User.GetId());

      return response.IsSuccess ? NoContent()
        : BadRequest(response.Error);
    }

    [HttpPost("{companyId}/Leave")]
    [Authorize]
    public async Task<ActionResult> LeaveCompanyAsync(Guid companyId)
    {
      var response = await companiesService.LeaveCompanyAsync(companyId, User.GetId());

      return response.IsSuccess ? NoContent()
        : BadRequest(response.Error);
    }

    [HttpPost("{companyId}/ChangeOwner")]
    [Authorize]
    public async Task<ActionResult> ChangeCompanyOwnerAsync(Guid companyId, Guid newOwnerId)
    {
      var response = await companiesService.ChangeOwner(companyId, User.GetId(), newOwnerId);

      return response.IsSuccess ? NoContent()
        : BadRequest(response.Error);
    }
  }
}
