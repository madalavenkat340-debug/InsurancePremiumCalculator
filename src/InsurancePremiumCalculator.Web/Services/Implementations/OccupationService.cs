using InsurancePremiumCalculator.Web.Models;
using InsurancePremiumCalculator.Web.Services.Interfaces;

namespace InsurancePremiumCalculator.Web.Services;

public class OccupationService : IOccupationService
{
    private static readonly List<Occupation> _occupations = new()
    {
        new Occupation("Cleaner","Light Manual",11.50),
        new Occupation("Doctor","Professional",1.5),
        new Occupation("Author","White Collar",2.25),
        new Occupation("Farmer","Heavy Manual",31.75),
        new Occupation("Mechanic","Heavy Manual",31.75),
        new Occupation("Florist","Light Manual",11.50),
        new Occupation("Other","Heavy Manual",31.75)
    };

    public List<Occupation> GetOccupations() => _occupations;
}
