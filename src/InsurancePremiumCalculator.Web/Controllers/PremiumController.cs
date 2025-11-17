using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        _premiumService = premiumService ?? throw new ArgumentNullException(nameof(premiumService));
        _occupationService = occupationService ?? throw new ArgumentNullException(nameof(occupationService));
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {        
        var occupations = await Task.FromResult(_occupationService.GetOccupations() ?? new List<Occupation>());
        ViewData["Occupations"] = occupations;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Calculate([FromForm] PremiumRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequestResponse(GetModelErrors());
        }        
        var occupations = await Task.FromResult(_occupationService.GetOccupations() ?? Enumerable.Empty<Occupation>());

        var occupation = occupations
            .FirstOrDefault(o => string.Equals(o?.Name, request.Occupation, StringComparison.OrdinalIgnoreCase));

        if (occupation == null)
        {
            return BadRequestResponse(new[] { "Invalid occupation" });
        }

        try
        {            
            var premium = await Task.Run(() =>
                _premiumService.CalculatePremium(request.DeathCoverAmount, occupation.RatingFactor, request.AgeNextBirthday)
            );

            return Ok(new PremiumResponse { Success = true, PremiumAmount = premium });
        }
        catch (ArgumentException ex)
        {
            return BadRequestResponse(new[] { ex.Message });
        }
    }

    private IEnumerable<string> GetModelErrors()
    {
        return ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .Where(msg => !string.IsNullOrWhiteSpace(msg))
            .Distinct()
            .ToList();
    }

    private BadRequestObjectResult BadRequestResponse(IEnumerable<string> errors)
    {
        var response = new PremiumResponse
        {
            Success = false,
            Errors = errors?.ToList() ?? new List<string> { "An unknown error occurred" }
        };
        return BadRequest(response);
    }
}