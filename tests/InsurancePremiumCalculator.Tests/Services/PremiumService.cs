using System;

namespace InsurancePremiumCalculator.Tests.Services
{
    internal class PremiumService
    {
        public decimal CalculatePremium(decimal deathCover, double factor, int age)
        {
            if (deathCover <= 0 || factor <= 0 || age <= 0)
                throw new ArgumentException("All input values must be greater than zero.");

            // Formula: (deathCover * factor * age) / 1000 * 12
            return Math.Round((deathCover * (decimal)factor * age) / 1000m * 12m, 2);
        }
    }
}