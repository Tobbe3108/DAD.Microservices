namespace Frontend.API.Features.Rental;

public record Rental(string StateAbbr,
  string BuildingNumber,
  string SecondaryAddress,
  string ZipCode,
  string City,
  string Country);