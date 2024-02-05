using Microsoft.EntityFrameworkCore;
using graphPlotter.Models;
using graphPlotter;

public class CurveFitContext : DbContext
{
  public CurveFitContext(DbContextOptions<CurveFitContext> options) : base(options) { }
  public DbSet<Point> Points { get; set; }

}

// namespace graphPlotter.Data
// {
//   public class CurveFitContext : DbContext
//   {
//     public CurveFitContext(DbContextOptions<CurveFitContext> options)
//         : base(options)
//     {
//     }

//     public DbSet<Point> Points { get; set; }
//     public DbSet<Plot> Plots { get; set; }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//       modelBuilder.Entity<Point>()
//           .HasOne(p => p.Plot)
//           .WithMany(b => b.Points)
//           .HasForeignKey(p => p.PlotId);

//       // Any additional configuration goes here

//       base.OnModelCreating(modelBuilder);
//     }
//   }
// }