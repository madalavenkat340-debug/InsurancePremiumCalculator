using InsurancePremiumCalculator.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePremiumCalculator.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class OccupationController : ControllerBase
{
    private readonly IOccupationService _occupationService;
    public OccupationController(IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var list = _occupationService.GetOccupations();
        return Ok(list);
    }
}
