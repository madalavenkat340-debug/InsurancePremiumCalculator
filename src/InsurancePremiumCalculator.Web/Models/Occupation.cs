namespace InsurancePremiumCalculator.Web.Models;

public class Occupation
{
    public string Name { get; set; } = string.Empty;
    public string Rating { get; set; } = string.Empty;
    public double RatingFactor { get; set; }

    public Occupation() { }
    public Occupation(string name, string rating, double factor)
    {
        Name = name;
        Rating = rating;
        RatingFactor = factor;
    }
}
