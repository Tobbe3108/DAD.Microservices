using Ardalis.ApiEndpoints;
using Bogus;
using Frontend.API.Features.Rental;
using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Frontend.API.Endpoints.Rental;

public class Put : EndpointBaseAsync.WithRequest<int>.WithActionResult
{
  private readonly ILogger<Put> _logger;
  private readonly IBus _bus;

  public Put(ILogger<Put> logger, IBus bus)
  {
    _logger = logger;
    _bus = bus;
  }

  [HttpPut("Rental/{Id:int}")]
  [SwaggerOperation(Summary = "Updates a Rental",
    Description = "Updates a Rental",
    OperationId = "Rental.Update",
    Tags = new[] { "Rental" })
  ]
  public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Publishing update rental command");

    var request = RentalFaker.GenerateFakeRental();

    var adapted = request.Adapt<UpdateRental>();
    adapted.Id = id;

    await _bus.Publish(adapted, cancellationToken);
    return Ok();
  }
}