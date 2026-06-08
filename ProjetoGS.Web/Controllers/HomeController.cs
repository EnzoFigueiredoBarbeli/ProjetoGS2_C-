using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoGS.Web.Models;
using System.Text.Json;

namespace ProjetoGS.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("ApiClient");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        try
        {
            var statsJson = await client.GetStringAsync("/api/tecnologias/stats");
            var stats = JsonSerializer.Deserialize<StatsViewModel>(statsJson, options) ?? new StatsViewModel();
            return View(stats);
        }
        catch
        {
            return View(new StatsViewModel());
        }
    }

    public IActionResult AcessoNegado() => View();
}
