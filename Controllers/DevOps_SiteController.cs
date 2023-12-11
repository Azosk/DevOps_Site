using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevOps_Site.Models;

namespace DevOps_Site.Controllers;

[ApiController]
[Route("[controller]")]
public class DevOps_SiteController : Controller
{
    private readonly ILogger<DevOps_SiteController> _logger;

    public DevOps_SiteController(ILogger<DevOps_SiteController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
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
    
    [HttpGet("Scripts")]
    public IActionResult Scripts()
    {
        return View("Views/DevOps_Site/Scripts/Scripts.cshtml");
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
