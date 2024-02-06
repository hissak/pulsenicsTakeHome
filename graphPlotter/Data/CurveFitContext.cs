using Microsoft.EntityFrameworkCore;
using graphPlotter; // Replace with the actual namespace where your models are located.

public class CurveFitContext : DbContext
{
  public CurveFitContext(DbContextOptions<CurveFitContext> options)
      : base(options)
  {
  }

  public DbSet<Point> Points { get; set; }
  public DbSet<Plot> Plots { get; set; }
  public DbSet<Curve> Curves { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Define the one-to-many relationship between Curve and Point
    modelBuilder.Entity<Curve>()
        .HasMany(c => c.Points) // Make sure Curve has a property Points which is a collection of Point
        .WithOne(p => p.Curve) // Make sure Point has a navigation property named Curve
        .HasForeignKey(p => p.CurveId); // Point should have a foreign key property named CurveId

    // If there are any other entity relationships or specific configurations, they can be set up here as well
  }
}
