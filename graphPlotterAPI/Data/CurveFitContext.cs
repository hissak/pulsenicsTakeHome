using Microsoft.EntityFrameworkCore;
using graphPlotterAPI;

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
    modelBuilder.Entity<Curve>()
        .HasMany(c => c.Points)
        .WithOne(p => p.Curve)
        .HasForeignKey(p => p.CurveId);
  }
}
