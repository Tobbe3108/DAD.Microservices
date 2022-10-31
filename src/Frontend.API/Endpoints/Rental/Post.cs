using Ardalis.ApiEndpoints;
using Frontend.API.Features.Rental;
using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Frontend.API.Endpoints.Rental;

public class Post : EndpointBaseAsync.WithoutRequest.WithActionResult
{
  private readonly ILogger<Post> _logger;
  private readonly IBus _bus;

  public Post(ILogger<Post> logger, IBus bus)
  {
    _logger = logger;
    _bus = bus;
  }

  [HttpPost("Rental")]
  [SwaggerOperation(Summary = "Create a new Rental",
    Description = "Creates a new Rental",
    OperationId = "Rental.Create",
    Tags = new[] { "Rental" })
  ]
  public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Publishing create rental command");
    var request = RentalFaker.GenerateFakeRental();
    await _bus.Publish(request.Adapt<AddRental>(), cancellationToken);
    return Ok();
  }
}