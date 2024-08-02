using EntityDataAPI.Models.Entity;

namespace EntityDataAPI.Repos.Entity;

public interface INameRepo
{
    Task<List<Name>> GetAllAsync();
    Task<List<Name>> GetNamesByEntityIdAsync(string entityId);
    Task<List<Name>> SearchAsync(string query);
    Task<Name?> GetByIdAsync(string id);
    Task AddAsync(Name name);
    Task UpdateAsync(Name name);
    Task DeleteAsync(string id);
}
