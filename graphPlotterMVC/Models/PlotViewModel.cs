using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace graphPlotter; // Replace with the actual namespace where your CurveType enum is located

// ViewModel for the plot input form
public class PlotViewModel
{
  [Required]
  public List<PointViewModel> Points { get; set; } = new List<PointViewModel>();

  [Required]
  public string CurveType { get; set; }

  // You can add other properties here if needed, such as a property to indicate if the plot should be archived
}

// ViewModel for individual points
public class PointViewModel
{
  [Required]
  [Display(Name = "X Value")]
  public double X { get; set; }

  [Required]
  [Display(Name = "Y Value")]
  public double Y { get; set; }
}

// // Enum to represent the type of curve
// public enum CurveType
// {
//   Linear,
//   Quadratic,
//   Cubic
//   // Add more curve types if necessary
// }
