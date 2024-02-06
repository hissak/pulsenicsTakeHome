using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using graphPlotter.Data;

namespace graphPlotter.Controllers
{
  public class HomeController : Controller
  {
    private readonly CurveFitContext _context;

    public HomeController(CurveFitContext context)
    {
      _context = context;
    }

    public IActionResult Index()
    {
      // Your logic here, e.g., passing data to the view
      return View();
    }

    // Other actions...
  }
}