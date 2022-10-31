using Refit;

namespace Frontend.API.Features.Rental;

public interface IRentalServiceApi
{
  [Get("/{id}")] Task<Rental?> GetRentalAsync(int id);
  [Get("/")] Task<IEnumerable<Rental>> GetRentalsAsync();
}