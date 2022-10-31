namespace Shared.Commands;

public class DeleteRental
{
  public DeleteRental(int id)
  {
    Id = id;
  }

  public int Id { get; set; }
}