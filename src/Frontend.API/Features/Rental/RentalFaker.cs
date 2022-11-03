using Bogus;

namespace Frontend.API.Features.Rental;

public static class RentalFaker
{
  public static Rental GenerateFakeRental() =>
    new Faker<Rental>()
      .Ignore(r => r.Id)
      .RuleFor(r => r.StateAbbr, f => f.Address.StateAbbr())
      .RuleFor(r => r.BuildingNumber, f => f.Address.BuildingNumber())
      .RuleFor(r => r.SecondaryAddress, f => f.Address.SecondaryAddress())
      .RuleFor(r => r.ZipCode, f => f.Address.ZipCode())
      .RuleFor(r => r.City, f => f.Address.City())
      .RuleFor(r => r.Country, f => f.Address.Country())
      .Generate();
}