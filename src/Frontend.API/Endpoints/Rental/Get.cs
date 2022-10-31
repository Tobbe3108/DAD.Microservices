using Ardalis.ApiEndpoints;
using Frontend.API.Features.Rental;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Frontend.API.Endpoints.Rental;

public class Get : EndpointBaseAsync.WithRequest<int>.WithActionResult
{
  private readonly ILogger<Get> _logger;
  private readonly IRentalServiceApi _rentalServiceApi;

  public Get(ILogger<Get> logger, IRentalServiceApi rentalServiceApi)
  {
    _logger = logger;
    _rentalServiceApi = rentalServiceApi;
  }

  [HttpGet("Rental/{Id:int}")]
  [SwaggerOperation(Summary = "Gets a Rental",
    Description = "Gets a Rental",
    OperationId = "Rental.Get",
    Tags = new[] { "Rental" })
  ]
  public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken = new())
  {
    _logger.LogInformation("Getting rental with {Id} from rental service api", id);
    return Ok(await _rentalServiceApi.GetRentalAsync(id));
  }
}