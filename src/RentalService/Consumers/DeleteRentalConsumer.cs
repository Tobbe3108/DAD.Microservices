using MassTransit;
using RentalService.Features.Database;
using Shared.Commands;
using Shared.Events;

namespace RentalService.Consumers;

public class DeleteRentalConsumer : IConsumer<DeleteRental>
{
  private readonly ILogger<DeleteRentalConsumer> _logger;
  private readonly RentalDbContext _rentalDbContext;

  public DeleteRentalConsumer(ILogger<DeleteRentalConsumer> logger, RentalDbContext rentalDbContext)
  {
    _logger = logger;
    _rentalDbContext = rentalDbContext;
  }

  public async Task Consume(ConsumeContext<DeleteRental> context)
  {
    _logger.LogInformation("Received {Command} command", nameof(DeleteRental));
    _logger.LogInformation("Removing rental with id {Id} from database", context.Message.Id);

    _rentalDbContext.Remove(new Rental { Id = context.Message.Id });
    await _rentalDbContext.SaveChangesAsync();

    await context.Publish(new RentalDeleted(context.Message.Id));
    _logger.LogInformation("Published {Event} event", nameof(RentalDeleted));
  }
}