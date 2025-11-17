using InsurancePremiumCalculator.Web.Models;
using InsurancePremiumCalculator.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePremiumCalculator.Web.Controllers;

public class PremiumController : Controller
{
    private readonly IPremiumService _premiumService;
    private readonly IOccupationService _occupationService;

    public PremiumController(IPremiumService premiumService, IOccupationService occupationService)
    {
        _premiumService = premiumService;
        _occupationService = occupationService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var occupations = _occupationService.GetOccupations();
        ViewData["Occupations"] = occupations;
        return View();
    }

    [HttpPost]
    public IActionResult Calculate([FromForm] PremiumRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new PremiumResponse { Success = false, Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
        }

        var occupation = _occupationService.GetOccupations().FirstOrDefault(o => o.Name == request.Occupation);
        if (occupation == null)
        {
            return BadRequest(new PremiumResponse { Success = false, Errors = new List<string> { "Invalid occupation" } });
        }

        try
        {
            var premium = _premiumService.CalculatePremium(request.DeathCoverAmount, occupation.RatingFactor, request.AgeNextBirthday);
            return Ok(new PremiumResponse { Success = true, PremiumAmount = premium });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new PremiumResponse { Success = false, Errors = new List<string> { ex.Message } });
        }
    }
}
