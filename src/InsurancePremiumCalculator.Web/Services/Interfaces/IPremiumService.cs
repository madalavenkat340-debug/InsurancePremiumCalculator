namespace InsurancePremiumCalculator.Web.Services.Interfaces;

public interface IPremiumService
{
    decimal CalculatePremium(decimal deathCoverAmount, double ratingFactor, int ageNextBirthday);
}
