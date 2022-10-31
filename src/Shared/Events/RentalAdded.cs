namespace Shared.Events;

public class RentalAdded
{
  public RentalAdded(int id)
  {
    Id = id;
  }
  public int Id { get; set; }
}