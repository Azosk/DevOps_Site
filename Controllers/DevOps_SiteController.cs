using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevOps_Site.Models;
using Microsoft.AspNetCore.Authorization;
using DevOps_Site.Data;

namespace DevOps_Site.Controllers;

[ApiController]
[Route("[controller]")]
public class DevOps_SiteController : Controller
{
    private readonly ILogger<DevOps_SiteController> _logger;

    private readonly ApplicationDbContext _context;

    public DevOps_SiteController(ILogger<DevOps_SiteController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult OnGet()
    {
        var scripts = _context.Scripts.AsEnumerable();
        if (_context.Scripts.Any())
        {
            return View("Views/Home/Index.cshtml", scripts);
        }
        return View("Views/Home/Index.cshtml");
    }

    [HttpGet("Git_GitHub")]
    public IActionResult Git()
    {
        return View("Views/DevOps_Site/Git_GitHub/Git_GitHub.cshtml");    }

    [HttpGet("APIs")]
    public IActionResult APIs()
    {
        return View("Views/DevOps_Site/APIs/APIs.cshtml");
    }

    [HttpGet("Azure")]
    public IActionResult Azure()
    {
        return View("Views/DevOps_Site/Azure/Azure.cshtml");
    }

    [HttpGet("Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
