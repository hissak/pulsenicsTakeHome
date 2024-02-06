namespace graphPlotter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Plot
{
  public int PlotId { get; set; }
  public string Title { get; set; }
  public Boolean IsArchived { get; set; }
  // Navigation property to Curve
  public Curve Curve { get; set; }
  // Add any additional metadata you might need for a plot
}