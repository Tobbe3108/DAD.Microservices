using Ardalis.ApiEndpoints;
using Frontend.API.Features.Rental;
using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Frontend.API.Endpoints.Rental;

public class List : EndpointBaseAsync.WithoutRequest.WithActionResult
{
  private readonly ILogger<List> _logger;
  private readonly IRentalServiceApi _rentalServiceApi;

  public List(ILogger<List> logger, IRentalServiceApi rentalServiceApi)
  {
    _logger = logger;
    _rentalServiceApi = rentalServiceApi;
  }

  [HttpGet("Rental")]
  [SwaggerOperation(Summary = "List Rentals",
    Description = "List Rentals",
    OperationId = "Rental.List",
    Tags = new[] { "Rental" })
  ]
  public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = new())
  {
    _logger.LogInformation("Getting all rentals from rental service api");
    return Ok(await _rentalServiceApi.GetRentalsAsync());
  }
}