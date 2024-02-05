using Microsoft.EntityFrameworkCore;
using graphPlotter;

namespace graphPlotter.Data
{
  public class CurveFitContext : DbContext
  {
    public CurveFitContext(DbContextOptions<CurveFitContext> options) : base(options) { }
    public DbSet<Point> Points { get; set; }
    public DbSet<Plot> Plots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Point>()
          .HasOne(p => p.Plot)
          .WithMany(c => c.Points)
          .HasForeignKey(p => p.PlotId);
    }
  }
}