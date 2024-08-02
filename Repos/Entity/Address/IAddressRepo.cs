using EntityDataAPI.Models.Entity;

namespace EntityDataAPI.Repos.Entity;

public interface IAddressRepo
{
    Task<List<Address>> GetAllAsync();
    IQueryable<Address> GetAllAsQueryable();
    Task<List<Address>> GetAddressesByEntityIdAsync(string entityId);
    Task<List<Address>> SearchAsync(string query);
    Task<Address?> GetByIdAsync(string id);
    Task AddAsync(Address address);
    Task UpdateAsync(Address address);
    Task DeleteAsync(string id);
}

