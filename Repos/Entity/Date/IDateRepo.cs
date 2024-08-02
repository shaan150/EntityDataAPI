using EntityDataAPI.Models.Entity;

namespace EntityDataAPI.Repos.Entity;

public interface IDateRepo
{
    Task<List<Date>> GetDatesByEntityIdAsync(string entityId);
    Task<Date?> GetByIdAsync(string id);
    Task AddAsync(Date date);
    Task UpdateAsync(Date date);
    Task DeleteAsync(string id);
}
