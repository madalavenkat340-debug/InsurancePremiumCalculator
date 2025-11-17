using InsurancePremiumCalculator.Web.Services.Interfaces;

namespace InsurancePremiumCalculator.Web.Services;

public class PremiumService : IPremiumService
{
    /// <summary>
    /// Calculates the annual premium using the formula:
    /// ((Death Cover Amount * Occupation Rating Factor * AgeNextBirthday) / 1000) * 12
    /// </summary>
    /// <param name="deathCoverAmount">Sum assured (must be &gt; 0).</param>
    /// <param name="ratingFactor">Occupation rating factor (must be &gt; 0).</param>
    /// <param name="ageNextBirthday">Age next birthday (must be between 1 and 120).</param>
    /// <returns>Annual premium rounded to 2 decimal places.</returns>
    public decimal CalculatePremium(decimal deathCoverAmount, double ratingFactor, int ageNextBirthday)
    {
        if (deathCoverAmount <= 0m)
            throw new ArgumentOutOfRangeException(nameof(deathCoverAmount), "Death cover amount must be > 0.");
        if (ratingFactor <= 0d)
            throw new ArgumentOutOfRangeException(nameof(ratingFactor), "Rating factor must be > 0.");
        if (ageNextBirthday <= 0 || ageNextBirthday > 120)
            throw new ArgumentOutOfRangeException(nameof(ageNextBirthday), "Age must be between 1 and 120.");

        // Formula: ((Death Cover amount * Occupation Rating Factor * Age) / 1000) * 12
        var annualPremium = ((deathCoverAmount * (decimal)ratingFactor * ageNextBirthday) / 1000m) * 12m;
        return decimal.Round(annualPremium, 2, MidpointRounding.AwayFromZero);
    }
}
