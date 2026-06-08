using Microsoft.AspNetCore.Mvc;
using ProjetoGS.ApiService.Models;
using ProjetoGS.ApiService.Repositories.Interfaces;

namespace ProjetoGS.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TecnologiasController : ControllerBase
{
    private readonly ITecnologiaRepository _repo;

    public TecnologiasController(ITecnologiaRepository repo)
    {
        _repo = repo;
    }

    // GET api/tecnologias
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _repo.GetAllAsync());

    // GET api/tecnologias/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tech = await _repo.GetByIdAsync(id);
        return tech is null ? NotFound() : Ok(tech);
    }

    // GET api/tecnologias/stats
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
        => Ok(await _repo.GetStatsAsync());

    // POST api/tecnologias
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Tecnologia tecnologia)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _repo.CreateAsync(tecnologia);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT api/tecnologias/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Tecnologia tecnologia)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _repo.UpdateAsync(id, tecnologia);
        return updated is null ? NotFound() : Ok(updated);
    }

    // DELETE api/tecnologias/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repo.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
