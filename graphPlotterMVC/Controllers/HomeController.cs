using Microsoft.AspNetCore.Mvc;
using graphPlotter; // Replace with the actual namespace where your models are located
using OxyPlot;
using OxyPlot.Series;
using MathNet.Numerics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// Here we will
// 1. Display the form to enter points
// 2. Handle the submission of the points
// 3. Process the selection of the curve types
// 4. Generate best fit curve (using Math.NET)
// 5. Generate the plot with OxyPlot

namespace graphPlotter.Controllers
{
  public class HomeController : Controller
  {
    private readonly CurveFitContext _context;

    public HomeController(CurveFitContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public IActionResult AddPoints(PlotViewModel plotViewModel)
    {
      if (ModelState.IsValid)
      {
        var curve = new Curve
        {
          Points = plotViewModel.Points.Select(p => new Point { X = p.X, Y = p.Y }).ToList(),
          CurveType = plotViewModel.CurveType
        };

        // var plot = new Plot
        // {
        //   Curve = curve,
        //   IsArchived = false // Initially, plots are not archived
        // };

        _context.Curves.Add(curve);
        _context.SaveChanges();

        // Redirect to the plotting action to show the plot
        return RedirectToAction("ShowPlot", new { id = curve.CurveId });
      }

      // If there are validation errors, show the form again
      return View("Index", plotViewModel);
    }
    // This is where the code from step 6.3 goes
    public IActionResult ShowPlot(int id)
    {
      var curve = _context.Curves.Include(p => p.Points).FirstOrDefault(p => p.CurveId == id);
      // var plot = _context.Plots.Include(p => p.Points).FirstOrDefault(p => p.Id == id);

      if (curve == null)
      {
        return NotFound();
      }

      var points = curve.Points.Select(p => new DataPoint(p.X, p.Y)).ToList();
      PlotModel plotModel = new PlotModel { Title = "Best Fit Curve" };

      // Depending on the curve type, fit the curve and add it to the plot model
      switch (curve.CurveType)
      {
        case "Linear":
          var linearFit = Fit.Line(points.Select(p => p.X).ToArray(), points.Select(p => p.Y).ToArray());
          plotModel.Series.Add(new FunctionSeries(x => linearFit.Item1 + linearFit.Item2 * x, points.Min(p => p.X), points.Max(p => p.X), 0.1));
          break;
          // Add cases for quadratic and cubic fits
          // ...
      }

      plotModel.Series.Add(new ScatterSeries { ItemsSource = points });

      // Save the plot model to the database if necessary
      // ...

      // Convert the plot model to an image or data format that can be displayed in the view
      // ...

      // Pass the plot model to the view
      return View(plotModel);
    }
  }
}