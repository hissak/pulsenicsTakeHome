using Microsoft.EntityFrameworkCore;
using graphPlotter;

public class CurveFitContext : DbContext
{
  public DbSet<Point> Points { get; set; }
  public DbSet<Plot> Plots { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=CurveFitDB.sqlite");
  }
}