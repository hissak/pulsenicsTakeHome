using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace graphPlotter.Data
{
  public class CurveFitContextFactory : IDesignTimeDbContextFactory<CurveFitContext>
  {
    public CurveFitContext CreateDbContext(string[] args)
    {
      IConfiguration configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var optionsBuilder = new DbContextOptionsBuilder<CurveFitContext>();
      optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));

      return new CurveFitContext(optionsBuilder.Options);
    }
  }
}