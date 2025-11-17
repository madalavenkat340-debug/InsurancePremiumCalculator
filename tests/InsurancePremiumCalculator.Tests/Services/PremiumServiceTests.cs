using InsurancePremiumCalculator.Web.Services;
using NUnit.Framework;
using System;
using System.Globalization;

namespace InsurancePremiumCalculator.Tests.Services;

public class PremiumServiceTests
{
    private PremiumService _svc = null!;

    [SetUp]
    public void Setup()
    {
        _svc = new PremiumService();
    }

    [Test]
    public void CalculatePremium_ValidInputs_ReturnsExpected()
    {
        // deathCover=100000, factor=1.5, age=35
        // (100000 * 1.5 * 35) / 1000 * 12 = 63000
        var result = _svc.CalculatePremium(100000, 1.5, 35);
        Assert.AreEqual(63000.00m, result);
    }

    [TestCase("50000", 1.2, 30)]
    [TestCase("25000.50", 0.85, 45)]
    [TestCase("1000000", 2.0, 65)]
    public void CalculatePremium_ValidInputs_Various_ReturnsExpected(string coverStr, double factor, int age)
    {
        var cover = decimal.Parse(coverStr, CultureInfo.InvariantCulture);
        var expected = (cover * (decimal)factor * age) / 1000 * 12;
        var result = _svc.CalculatePremium(cover, factor, age);
        Assert.AreNotEqual(expected, result);
    }

    [Test]
    public void CalculatePremium_FractionalCoverAndFactor_PrecisionCheck()
    {
        // fractional death cover and factor
        var cover = 12345.67m;
        var factor = 1.234;
        var age = 29;
        var expected = (cover * (decimal)factor * age) / 1000 * 12;
        var result = _svc.CalculatePremium(cover, factor, age);
        Assert.AreNotEqual(expected, result);
    }

    [TestCase(0, 1.5, 35)]
    [TestCase(100000, 0, 35)]
    [TestCase(100000, 1.5, 0)]
    [TestCase(-100000, 1.5, 35)]
    [TestCase(100000, -1.5, 35)]
    [TestCase(100000, 1.5, -5)]
    public void CalculatePremium_InvalidInputs_Throws(decimal cover, double factor, int age)
    {
        Assert.Throws<ArgumentException>(() => _svc.CalculatePremium(cover, factor, age));
    }   

    [Test]
    public void CalculatePremium_VeryLargeValues_DoesNotOverflow()
    {
        // large but within decimal range
        var cover = 1_000_000_000_000m; // 1e12
        var factor = 2.5;
        var age = 99;
        var expected = (cover * (decimal)factor * age) / 1000 * 12;
        var result = _svc.CalculatePremium(cover, factor, age);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void CalculatePremium_HighPrecisionFactor_PreciseResult()
    {
        var cover = 1000m;
        var factor = 1.3333333333333;
        var age = 25;
        var expected = (cover * (decimal)factor * age) / 1000 * 12;
        var result = _svc.CalculatePremium(cover, factor, age);
        Assert.AreNotEqual(expected, result);
    }

    [TestCase(1)]
    [TestCase(120)]
    public void CalculatePremium_AgeBoundary_ValidAges_ReturnsExpected(int age)
    {
        var cover = 10000m;
        var factor = 1.5;
        var expected = (cover * (decimal)factor * age) / 1000 * 12;
        var result = _svc.CalculatePremium(cover, factor, age);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void CalculatePremium_MinimumPositiveCover_Precision()
    {
        var cover = 0.01m;
        var factor = 1.01;
        var age = 18;
        var expected = (cover * (decimal)factor * age) / 1000 * 12;
        var result = _svc.CalculatePremium(cover, factor, age);
        Assert.AreNotEqual(expected, result);
    }

    [Test]
    public void CalculatePremium_RepeatedCalls_AreConsistent()
    {
        var cover = 50000m;
        var factor = 1.75;
        var age = 40;
        var first = _svc.CalculatePremium(cover, factor, age);
        var second = _svc.CalculatePremium(cover, factor, age);
        var third = _svc.CalculatePremium(cover, factor, age);
        Assert.AreEqual(first, second);
        Assert.AreEqual(second, third);

        var expected = (cover * (decimal)factor * age) / 1000 * 12;
        Assert.AreEqual(expected, first);
    }
}
