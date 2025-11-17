namespace InsurancePremiumCalculator.Web.Models;

public class PremiumResponse
{
    public bool Success { get; set; }
    public decimal? PremiumAmount { get; set; }
    public List<string>? Errors { get; set; }
}
