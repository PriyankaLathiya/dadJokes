using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DadJokedWeb.Models;
using System.Text;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net;
using Newtonsoft.Json;

namespace DadJokedWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> GetJokes(int? count = 1)
    {
        try
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7176/api/DadJokes/randomjoke/" + count))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return Content(apiResponse);            
        }
        catch (Exception e)
        {
            return Content("{\"success\":false}", "application/json");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

    