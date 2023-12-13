using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevOps_Site.Models;
using DevOps_Site.Data;

namespace DevOps_Site.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDbContext _context;
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var scripts = _context.Scripts.AsEnumerable();
        if (_context.Scripts.Any())
        {
            return View("Views/Home/Index.cshtml", scripts);
        }
        return View("Views/Home/Index.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
