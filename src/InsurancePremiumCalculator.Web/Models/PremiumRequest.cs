using System.ComponentModel.DataAnnotations;

namespace InsurancePremiumCalculator.Web.Models;

public class PremiumRequest
{
    [Required]
    public string? Name { get; set; }

    [Required]
    [Range(1, 120)]
    public int AgeNextBirthday { get; set; }

    [Required]
    [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{4}$", ErrorMessage = "DOB must be in MM/YYYY format")]
    public string? DateOfBirth { get; set; }

    [Required]
    public string? Occupation { get; set; }

    [Required]
    [Range(1, double.MaxValue)]
    public decimal DeathCoverAmount { get; set; }
}
