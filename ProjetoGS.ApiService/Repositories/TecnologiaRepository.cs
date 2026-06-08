using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Data;
using ProjetoGS.ApiService.Models;
using ProjetoGS.ApiService.Repositories.Interfaces;

namespace ProjetoGS.ApiService.Repositories;

public class TecnologiaRepository : ITecnologiaRepository
{
    private readonly AppDbContext _context;

    public TecnologiaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tecnologia>> GetAllAsync()
        => await _context.Tecnologias.Include(t => t.Categoria).OrderByDescending(t => t.DataCadastro).ToListAsync();

    public async Task<Tecnologia?> GetByIdAsync(int id)
        => await _context.Tecnologias.Include(t => t.Categoria).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<Tecnologia> CreateAsync(Tecnologia tecnologia)
    {
        tecnologia.DataCadastro = DateTime.UtcNow;
        _context.Tecnologias.Add(tecnologia);
        await _context.SaveChangesAsync();
        return tecnologia;
    }

    public async Task<Tecnologia?> UpdateAsync(int id, Tecnologia tecnologia)
    {
        var existing = await _context.Tecnologias.FindAsync(id);
        if (existing is null) return null;

        existing.Nome               = tecnologia.Nome;
        existing.Descricao          = tecnologia.Descricao;
        existing.OrigemMissao       = tecnologia.OrigemMissao;
        existing.AnoDesenvolvimento = tecnologia.AnoDesenvolvimento;
        existing.CategoriaId        = tecnologia.CategoriaId;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Tecnologias.FindAsync(id);
        if (existing is null) return false;

        _context.Tecnologias.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<object> GetStatsAsync()
    {
        var totalTecnologias = await _context.Tecnologias.CountAsync();
        var totalMissoes     = await _context.Tecnologias.Select(t => t.OrigemMissao).Distinct().CountAsync();
        var totalSetores     = await _context.Categorias.CountAsync();

        var porCategoria = await _context.Categorias
            .Select(c => new
            {
                Categoria = c.Nome,
                Total     = c.Tecnologias.Count
            })
            .ToListAsync();

        var recentes = await _context.Tecnologias
            .Include(t => t.Categoria)
            .OrderByDescending(t => t.DataCadastro)
            .Take(5)
            .Select(t => new
            {
                t.Id,
                t.Nome,
                t.OrigemMissao,
                Categoria    = t.Categoria!.Nome,
                t.DataCadastro
            })
            .ToListAsync();

        return new
        {
            TotalTecnologias = totalTecnologias,
            TotalMissoes     = totalMissoes,
            TotalSetores     = totalSetores,
            PorCategoria     = porCategoria,
            Recentes         = recentes
        };
    }
}
