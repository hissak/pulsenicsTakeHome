namespace graphPlotterAPI;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Point
{
  public int PointId { get; set; }
  public double X { get; set; }
  public double Y { get; set; }
  public int CurveId { get; set; }
  public Curve Curve { get; set; }

}