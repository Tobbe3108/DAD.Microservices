using Mapster;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RentalService.Features.Database;
using Shared.Commands;
using Shared.Events;

namespace RentalService.Consumers;

public class UpdateRentalConsumer : IConsumer<UpdateRental>
{
  private readonly ILogger<UpdateRentalConsumer> _logger;
  private readonly RentalDbContext _rentalDbContext;

  public UpdateRentalConsumer(ILogger<UpdateRentalConsumer> logger, RentalDbContext rentalDbContext)
  {
    _logger = logger;
    _rentalDbContext = rentalDbContext;
  }

  public async Task Consume(ConsumeContext<UpdateRental> context)
  {
    _logger.LogInformation("Received {Command} command", nameof(UpdateRental));
    _logger.LogInformation("Updating rental with id {Id} in database", context.Message.Id);

    var dbModel = await _rentalDbContext.Rentals.FirstOrDefaultAsync(rental => rental.Id == context.Message.Id);
    ArgumentNullException.ThrowIfNull(dbModel);

    context.Message.Adapt(dbModel);
    await _rentalDbContext.SaveChangesAsync();

    await context.Publish(new RentalDeleted(context.Message.Id));
    _logger.LogInformation("Published {Event} event", nameof(RentalDeleted));
  }
}