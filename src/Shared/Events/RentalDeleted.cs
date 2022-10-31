namespace Shared.Events;

public class RentalDeleted
{
  public RentalDeleted(int id)
  {
    Id = id;
  }
  public int Id { get; set; }
}