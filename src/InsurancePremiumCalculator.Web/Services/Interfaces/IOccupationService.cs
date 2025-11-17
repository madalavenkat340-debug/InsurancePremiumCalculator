using InsurancePremiumCalculator.Web.Models;

namespace InsurancePremiumCalculator.Web.Services.Interfaces;

public interface IOccupationService
{
    List<Occupation> GetOccupations();
}
