using Shared.EFCore;

// ReSharper disable UnusedMember.Global
#pragma warning disable CS8618

namespace RentalService.Features.Database;

public class Rental : SchemaBase
{
  public string StateAbbr { get; set; }
  public string BuildingNumber { get; set; }
  public string SecondaryAddress { get; set; }
  public string ZipCode { get; set; }
  public string City { get; set; }
  public string Country { get; set; }
}