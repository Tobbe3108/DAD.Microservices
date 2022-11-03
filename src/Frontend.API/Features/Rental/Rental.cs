#pragma warning disable CS8618
namespace Frontend.API.Features.Rental;

public class Rental
{
  public int Id { get; set; }
  public string StateAbbr { get; set; }
  public string BuildingNumber { get; set; }
  public string SecondaryAddress { get; set; }
  public string ZipCode { get; set; }
  public string City { get; set; }
  public string Country { get; set; }
}