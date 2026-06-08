using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoGS.Web.Models;
using System.Text;
using System.Text.Json;

namespace ProjetoGS.Web.Controllers;

[Authorize]
public class TecnologiasController : Controller
{
    private readonly IHttpClientFactory _factory;
    private readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    public TecnologiasController(IHttpClientFactory factory) => _factory = factory;

    private HttpClient Client => _factory.CreateClient("ApiClient");

    public async Task<IActionResult> Index()
    {
        var json = await Client.GetStringAsync("/api/tecnologias");
        var lista = JsonSerializer.Deserialize<List<TecnologiaViewModel>>(json, _opts) ?? new();
        return View(lista);
    }

    public async Task<IActionResult> Details(int id)
    {
        var json = await Client.GetStringAsync($"/api/tecnologias/{id}");
        var tech = JsonSerializer.Deserialize<TecnologiaViewModel>(json, _opts);
        return tech is null ? NotFound() : View(tech);
    }

    // Apenas Admin pode criar/deletar
    [Authorize(Policy = "ApenasAdmin")]
    public IActionResult Create() => View(new TecnologiaViewModel());

    [HttpPost, Authorize(Policy = "ApenasAdmin"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TecnologiaViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        await Client.PostAsync("/api/tecnologias", content);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "ApenasAdmin")]
    public async Task<IActionResult> Edit(int id)
    {
        var json = await Client.GetStringAsync($"/api/tecnologias/{id}");
        var tech = JsonSerializer.Deserialize<TecnologiaViewModel>(json, _opts);
        return tech is null ? NotFound() : View(tech);
    }

    [HttpPost, Authorize(Policy = "ApenasAdmin"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TecnologiaViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        await Client.PutAsync($"/api/tecnologias/{id}", content);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "ApenasAdmin")]
    public async Task<IActionResult> Delete(int id)
    {
        var json = await Client.GetStringAsync($"/api/tecnologias/{id}");
        var tech = JsonSerializer.Deserialize<TecnologiaViewModel>(json, _opts);
        return tech is null ? NotFound() : View(tech);
    }

    [HttpPost, ActionName("Delete"), Authorize(Policy = "ApenasAdmin"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await Client.DeleteAsync($"/api/tecnologias/{id}");
        return RedirectToAction(nameof(Index));
    }
}
