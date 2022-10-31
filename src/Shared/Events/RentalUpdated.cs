namespace Shared.Events;

public class RentalUpdated
{
  public RentalUpdated(int id)
  {
    Id = id;
  }
  public int Id { get; set; }
}