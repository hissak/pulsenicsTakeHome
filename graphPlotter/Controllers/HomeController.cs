using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MathNet.Numerics;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.Optimization.ObjectiveFunctions;
using graphPlotter;
using ChartJs.Blazor.ChartJS.Common.Axes;
using ChartJs.Blazor.ChartJS.Common.Enums;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using ChartJs.Blazor.ChartJS.LineChart;

public class HomeController : Controller
{
  private readonly CurveFitContext _context;

  public HomeController(CurveFitContext context)
  {
    _context = context;
  }

  public IActionResult Index()
  {
    var achievedPlots = _context.Plots.ToList();
    return View(achievedPlots);
  }

  [HttpPost]
  public IActionResult AddPoint(Point point)
  {
    if (ModelState.IsValid)
    {
      _context.Points.Add(point);
      _context.SaveChanges();
    }
    return RedirectToAction("Index");
  }

  [HttpPost]
  public IActionResult CalculateBestFit(int degree)
  {
    var points = _context.Points.ToList();
    var xData = points.Select(p => p.X).ToArray();
    var yData = points.Select(p => p.Y).ToArray();

    var coefficients = Fit.Polynomial(xData, yData, degree);
    var equation = string.Join(" + ", coefficients.Select((c, i) => $"{c:F2}x^{degree - i}"));

    var plotImage = GeneratePlotImage(xData, yData, coefficients);
    var plot = new Plot { Equation = equation, PlotImage = plotImage };

    _context.Plots.Add(plot);
    _context.SaveChanges();

    return RedirectToAction("Index");
  }

  public IActionResult GeneratePlotImage()
  {
    // Retrieve data from the database or generate sample data
    var points = _context.Points.ToList(); // Assuming you have a Points DbSet in your context
    var xData = points.Select(p => p.X).ToArray();
    var yData = points.Select(p => p.Y).ToArray();

    // Perform curve fitting and generate best fit data
    var bestFitData = FitBestCurve(xData, yData);

    // Create a Chart.js LineChart
    var lineChart = new LineConfig
    {
      Options = new LineOptions
      {
        Responsive = true,
        Plugins = new LinePlugins
        {
          Title = new PluginTitle
          {
            Display = true,
            Text = "Best Fit Curve"
          }
        },
        Scales = new Scales
        {
          XAxes = new List<CartesianAxis> { new CartesianLinearAxis { ScaleLabel = new ScaleLabel { LabelString = "X Axis" } } },
          YAxes = new List<CartesianAxis> { new CartesianLinearAxis { ScaleLabel = new ScaleLabel { LabelString = "Y Axis" } } }
        }
      }
    };

    // Add data sets to the chart
    lineChart.Data.Labels.AddRange(xData.Select(x => x.ToString()));
    lineChart.Data.Datasets.Add(new LineDataset<double>(yData.ToList()) { Label = "Data Points", BackgroundColor = ColorUtil.RandomColorString() });
    lineChart.Data.Datasets.Add(new LineDataset<double>(bestFitData.ToList()) { Label = "Best Fit Curve", BackgroundColor = ColorUtil.RandomColorString() });

    // Convert the LineChart to a BaseChart (parent of all chart types)
    var baseChart = lineChart.ToBaseChart();

    // Convert the BaseChart to a BaseHandler (parent of all chart handlers)
    var baseHandler = baseChart.ToBaseHandler();

    // Render the BaseHandler to an image stream
    var imageStream = baseHandler.BaseRender(800, 400);

    // Return the image stream as a File result
    return File(imageStream, "image/png");
  }

  private double[] FitBestCurve(double[] xData, double[] yData)
  {
    // Implement your curve fitting logic here
    // Example: Fit a linear regression using Math.NET Numerics
    var coefficients = Fit.LinearCombination(xData, yData, w => 1, w => w);

    // Generate best fit data for the plot
    var bestFitData = xData.Select(x => coefficients[0] + coefficients[1] * x).ToArray();

    return bestFitData;
  }
}