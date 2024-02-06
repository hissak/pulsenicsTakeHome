namespace graphPlotter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Point
{
  // public DbSet<Point> Points { get; set; }
  public int PointId { get; set; }
  public double X { get; set; }
  public double Y { get; set; }
  // You can add a foreign key to associate each point with a Plot if necessary
  public int CurveId { get; set; }
  public Curve Curve { get; set; }

}