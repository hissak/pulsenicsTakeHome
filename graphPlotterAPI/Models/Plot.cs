namespace graphPlotterAPI;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Plot
{
  public int PlotId { get; set; }
  public string Title { get; set; }
  public Boolean IsArchived { get; set; }
  // public Curve Curve { get; set; }
}