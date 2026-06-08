using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProjetoGS.Web.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ProjetoGS.Web.Controllers;

public class AccountController : Controller
{
    private readonly IHttpClientFactory _factory;
    private readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    public AccountController(IHttpClientFactory factory) => _factory = factory;

    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var client  = _factory.CreateClient("ApiClient");
        var payload = JsonSerializer.Serialize(new { model.Email, Senha = model.Senha });
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/usuarios/login", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Email ou senha inválidos.");
            return View(model);
        }

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var nome   = root.GetProperty("nome").GetString() ?? "";
        var email  = root.GetProperty("email").GetString() ?? "";
        var perfil = root.GetProperty("perfil").GetString() ?? "Pesquisador";

        // Claims — perfil injetado via Cookie
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name,  nome),
            new(ClaimTypes.Email, email),
            new("Perfil",         perfil)
        };

        var identity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Cadastro() => View(new CadastroViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastro(CadastroViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var client  = _factory.CreateClient("ApiClient");
        var payload = JsonSerializer.Serialize(new { model.Nome, model.Email, Senha = model.Senha });
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/usuarios/cadastro", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Erro ao cadastrar. Email já pode estar em uso.");
            return View(model);
        }

        TempData["Sucesso"] = "Cadastro realizado! Faça login para continuar.";
        return RedirectToAction(nameof(Login));
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }

    public IActionResult AcessoNegado() => View();
}
