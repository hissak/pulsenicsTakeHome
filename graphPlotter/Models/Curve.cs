namespace graphPlotter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Curve
{
  public int CurveId { get; set; }
  public string CurveType { get; set; } // "linear", "quadratic", "cubic"
  public string Equation { get; set; } // Store the equation as a string
  public List<Point> Points { get; set; } = new List<Point>(); // Navigation property
                                                               // Coefficients can be stored in a JSON string or as individual properties
  public string Coefficients { get; set; }
}
