namespace EntityDataAPI.Repos.Entity;

using EntityDataAPI.DTOs;
using EntityDataAPI.Filters;
using EntityDataAPI.Models.Entity;

public interface IEntityRepo
{
    Task<List<Entity>> GetAllAsync();
    IQueryable<Entity> GetAllAsQueryable();
    Task<Entity?> GetByIdAsync(string id);
    Task<List<Entity?>> GetByIdsAsync(HashSet<string> ids, SearchFilter searchFilter);
    Task<HashSet<string>> SearchAsync(SearchFilter search);
    Task AddAsync(Entity entity);
    Task UpdateAsync(Entity entity);
    Task DeleteAsync(string id);
}
