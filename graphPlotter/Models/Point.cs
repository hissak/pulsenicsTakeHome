namespace graphPlotter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Point
{
  [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int ID { get; set; }
  public double X { get; set; }
  public double Y { get; set; }
  public int PlotId { get; set; }
  public Plot? Plot { get; set; }
}
