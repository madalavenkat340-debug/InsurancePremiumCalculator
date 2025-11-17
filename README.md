# Insurance Premium Calculator - .NET 8 MVC (NUnit)

## Overview
This is a complete skeleton project for the coding test, targeting **.NET 8** and using **NUnit** for unit tests.

- ASP.NET Core MVC project (C#)
- Service layer implementation for premium calculation
- API stub that returns occupations and rating factors
- Razor view (Index) with JavaScript that triggers premium calculation whenever the occupation dropdown changes
- NUnit unit tests for the PremiumService

## Premium formula
Death Premium = (Death Cover Amount * Rating Factor * Age) / 1000 * 12

## How to run (locally)
1. Install .NET 8 SDK from https://dotnet.microsoft.com
2. From the `src/InsurancePremiumCalculator.Web` folder:
   - `dotnet restore`
   - `dotnet run`
3. Open `https://localhost:5001` (or the URL shown).

## Unit Tests
From the repository root:
```
dotnet test
```

## Notes & Assumptions
- Occupation list is served by an API stub (no DB required).
- All input fields are mandatory. Basic validation is implemented on server side.
- The UI triggers the premium calculation on occupation dropdown change as requested.
