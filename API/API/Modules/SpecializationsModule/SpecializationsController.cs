using API.Modules.SpecializationsModule.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.SpecializationsModule;

[Route("api/[controller]")]
[ApiController]
public class SpecializationsController : ControllerBase
{
  private readonly ISpecializationsService specializationsService;

  public SpecializationsController(ISpecializationsService specializationsService)
  {
    this.specializationsService = specializationsService;
  }

  [HttpGet]
  public ActionResult GetSpecializations()
  {
    var response = specializationsService.GetSpecializations();

    return response.IsSuccess
      ? Ok(response.Value)
      : BadRequest(response.Error);
  }
}
