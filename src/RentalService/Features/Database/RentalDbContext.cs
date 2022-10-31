using Microsoft.EntityFrameworkCore;

namespace RentalService.Features.Database;

public class RentalDbContext : DbContext
{
  public RentalDbContext(DbContextOptions<RentalDbContext> options)
    : base(options) { }

  public DbSet<Rental> Rentals { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Rental>(entity =>
    {
      entity.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();
      entity.HasIndex(e => e.Id).IsUnique();
    });
  }
}