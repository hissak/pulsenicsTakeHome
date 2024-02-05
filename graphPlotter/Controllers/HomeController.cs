using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MathNet.Numerics;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.Optimization.ObjectiveFunctions;
using graphPlotter;

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

    var plotImage = /* Generate plot image using a chart library */;
    var plot = new Plot { Equation = equation, PlotImage = plotImage };

    _context.Plots.Add(plot);
    _context.SaveChanges();

    return RedirectToAction("Index");
  }
}