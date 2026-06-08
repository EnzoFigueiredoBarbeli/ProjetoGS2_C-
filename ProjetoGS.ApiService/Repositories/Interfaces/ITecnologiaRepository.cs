using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories.Interfaces;

public interface ITecnologiaRepository
{
    Task<IEnumerable<Tecnologia>> GetAllAsync();
    Task<Tecnologia?> GetByIdAsync(int id);
    Task<Tecnologia> CreateAsync(Tecnologia tecnologia);
    Task<Tecnologia?> UpdateAsync(int id, Tecnologia tecnologia);
    Task<bool> DeleteAsync(int id);
    Task<object> GetStatsAsync();
}
