using Mapster;
using MassTransit;
using RentalService.Features.Database;
using Shared.Commands;
using Shared.Events;

namespace RentalService.Consumers;

public class AddRentalConsumer : IConsumer<AddRental>
{
  private readonly ILogger<AddRentalConsumer> _logger;
  private readonly RentalDbContext _rentalDbContext;

  public AddRentalConsumer(ILogger<AddRentalConsumer> logger, RentalDbContext rentalDbContext)
  {
    _logger = logger;
    _rentalDbContext = rentalDbContext;
  }

  public async Task Consume(ConsumeContext<AddRental> context)
  {
    _logger.LogInformation("Received {Command} command", nameof(AddRental));
    _logger.LogInformation("Adding rental to database");

    var adapted = context.Message.Adapt<Rental>();
    await _rentalDbContext.AddAsync(adapted);
    await _rentalDbContext.SaveChangesAsync();

    await context.Publish(new RentalAdded(adapted.Id));
    _logger.LogInformation("Published {Event} event", nameof(RentalAdded));
  }
}