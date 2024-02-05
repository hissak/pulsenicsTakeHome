namespace graphPlotter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Plot
{
  [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int PlotId { get; set; }
  public string? Equation { get; set; } // Linear, Quadratic, Cubic curves
  public byte[]? PlotImage { get; set; }

  public ICollection<Point>? Points { get; set; }
}
