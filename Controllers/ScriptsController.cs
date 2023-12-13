using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevOps_Site.Models;
using Microsoft.AspNetCore.Authorization;
using DevOps_Site.Data;
using Microsoft.EntityFrameworkCore;

namespace DevOps_Site.Controllers;

[ApiController]
[Route("DevOps_Site/[controller]/[action]")]
public class ScriptController : Controller
{
    private readonly ILogger<DevOps_SiteController> _logger;

    private readonly ApplicationDbContext _context;

    public ScriptController(ILogger<DevOps_SiteController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    [HttpGet("/DevOps_Site/Scripts")]
    [Authorize]
    public IActionResult Scripts(string? searchString, string? sortOrder)
    {

        if(_context.Scripts == null)
        {
            return Problem("Scripts NULL");
        }

        var scriptsSearch = from s in _context.Scripts
                    select s;
        var scriptcontext = _context.Scripts.AsEnumerable();

        // Apply the sort order if specified
        switch (sortOrder)
        {
            case "Newest":
                scriptsSearch = scriptsSearch.OrderByDescending(s => s.AuthorDate);
                scriptcontext = _context.Scripts.OrderByDescending(s => s.AuthorDate);
                break;
            case "Oldest":
                scriptsSearch = scriptsSearch.OrderBy(s => s.AuthorDate);
                scriptcontext = _context.Scripts.OrderBy(s => s.AuthorDate);
                break;
            default:
                // Keep the original order
                break;
        }

        if (!String.IsNullOrEmpty(searchString))
        {
            scriptsSearch = scriptsSearch.Where(s => s.ScriptName!.Contains(searchString));

            return View("Views/DevOps_Site/Scripts/Scripts.cshtml", scriptsSearch.AsEnumerable());
        }
        if (_context.Scripts.Any())
        {
            return View("Views/DevOps_Site/Scripts/Scripts.cshtml", scriptcontext);
        }
        else
        {
            return View("Views/DevOps_Site/Scripts/Scripts.cshtml");
        }
    }

    // Get the script create page
    [HttpGet("/DevOps_Site/Scripts/Create")]
    [Authorize]
    public IActionResult Create()
    {

        // Pass the model to the view
        return View("Views/DevOps_Site/Scripts/Create.cshtml");
    }

    [HttpPost]
    public IActionResult Script_Create([FromForm] Script script)
    {
    // Validate the model
    if (ModelState.IsValid)
    {
        // Set the creation date from the environment
        script.AuthorDate = DateTime.Now;
        // Save the script to the database using EF context
        _context.Scripts.Add(script);
        _context.SaveChanges();
        // Redirect to the script list or details page
        return Redirect("http://localhost:5172/DevOps_Site/Scripts/");    
    }
    else
    {
        // If the model is invalid, return the same view with the model
        return Redirect("http://localhost:5172//DevOps_Site/Scripts/Create.cshtml");
    }
    }

    [HttpGet("/DevOps_Site/Scripts/Delete/{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var script = await _context.Scripts
            .FirstOrDefaultAsync(s => s.ScriptID == id);
        
        if (script == null)
        {
            return NotFound();
        }

        return View("Views/DevOps_Site/Scripts/Delete.cshtml", script);
    }
    
    // POST: DevOps_Site/Script/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Script_Delete([FromForm]int id)
    {
        if (ModelState.IsValid)
        {
            var script = await _context.Scripts.FirstOrDefaultAsync(s => s.ScriptID == id);
            if (script != null)
            {
                _context.Scripts.Remove(script);
                await _context.SaveChangesAsync();
                // Redirect to the script list or details page
                return Redirect("http://localhost:5172/DevOps_Site/Scripts/"); 
            }
        }
            return Redirect("http://localhost:5172/DevOps_Site/Scripts/"); 
    }

    [HttpGet("/DevOps_Site/Scripts/Edit/{id}")]
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var script = await _context.Scripts
            .FirstOrDefaultAsync(s => s.ScriptID == id);
        
        if (script == null)
        {
            return NotFound();
        }

        return View("Views/DevOps_Site/Scripts/Edit.cshtml", script);
    }
    
    // POST: DevOps_Site/Script/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Script_Edit([FromForm]Script editedScript)
    {
        if (ModelState.IsValid)
        {
        var script = _context.Scripts.FirstOrDefault(s => s.ScriptID == editedScript.ScriptID);
            if (script != null)
            {
                script.ScriptContent = editedScript.ScriptContent;
                if(await TryUpdateModelAsync(script, "editedScript"))
                {
                    _context.Entry(script).Property("ScriptContent").IsModified = true;
                    await _context.SaveChangesAsync();
                    return Redirect("http://localhost:5172/DevOps_Site/Scripts/");   
                }
                else
                {
                    return View("Views/DevOps_Site/");
                }   
            }
            else
            {
                // Redirect to the script list or details page
                return Redirect("http://localhost:5172/DevOps_Site/Scripts/"); 
            }
        }
        // Redirect to the script list or details page
        return Redirect("http://localhost:5172/DevOps_Site/Scripts/"); 
    }

    [HttpGet("/DevOps_Site/Scripts/Details/{id}")]
    [Authorize]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var script = await _context.Scripts
            .FirstOrDefaultAsync(s => s.ScriptID == id);
        
        if (script == null)
        {
            return NotFound();
        }

        return View("Views/DevOps_Site/Scripts/Details.cshtml", script);
    }

    [HttpGet("Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
