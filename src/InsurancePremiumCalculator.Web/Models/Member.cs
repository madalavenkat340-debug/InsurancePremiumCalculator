using System;

namespace InsurancePremiumCalculator.Web.Models;

public class Member
{
    public int MemberId { get; set; }
    public string? Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int AgeNextBirthday { get; set; }
    public string? Occupation { get; set; }
    public decimal DeathCoverAmount { get; set; }
}
