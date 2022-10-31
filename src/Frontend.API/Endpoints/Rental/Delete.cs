using Ardalis.ApiEndpoints;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Frontend.API.Endpoints.Rental;

public class Delete : EndpointBaseAsync.WithRequest<int>.WithActionResult
{
  private readonly ILogger<Delete> _logger;
  private readonly IBus _bus;

  public Delete(ILogger<Delete> logger, IBus bus)
  {
    _logger = logger;
    _bus = bus;
  }

  [HttpDelete("Rental/{Id:int}")]
  [SwaggerOperation(Summary = "Deletes a Rental",
    Description = "Deletes a Rental",
    OperationId = "Rental.Delete",
    Tags = new[] { "Rental" })
  ]
  public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken = new())
  {
    _logger.LogInformation("Publishing remove rental command");
    await _bus.Publish(new DeleteRental(id), cancellationToken);
    return Ok();
  }
}