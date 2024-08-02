using EntityDataAPI.Data;
using EntityDataAPI.Exceptions.Operations;
using EntityDataAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;
using static EntityDataAPI.Utils.UtilityHelpers;


namespace EntityDataAPI.Repos.Entity;

public class AddressRepo : IAddressRepo
{
    private readonly AppDbContext _context;

    public AddressRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Address>> GetAllAsync()
    {
        return await _context.Addresses.ToListAsync();
    }

    public IQueryable<Address> GetAllAsQueryable()
    {
        return _context.Addresses.AsQueryable();
    }

    public async Task<List<Address>> SearchAsync(string query)
    {
        return await _context.Addresses
                            .Where(a => EF.Functions.Like(a.AddressLine, $"%{query}%")
                                    || EF.Functions.Like(a.Country, $"%{query}%"))
                            .ToListAsync();
    }

    public async Task<Address?> GetByIdAsync(string id)
    {
        return await _context.Addresses.FindAsync(id);
    }

    public async Task AddAsync(Address address)
    {
        await HandleOperationAsync(
            async () =>
            {
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
            },
            "Error occurred while adding the address"
        );
    }

    public async Task UpdateAsync(Address address)
    {
        await HandleOperationAsync(
            async () =>
            {
                var existingAddress = await _context.Addresses.FindAsync(address.AddressID) ?? throw new OperationException("Address not found.");
                _context.Addresses.Update(address);
                await _context.SaveChangesAsync();
            },
            "Error occurred while updating the address"
        );
    }

    public async Task DeleteAsync(string id)
    {
        await HandleOperationAsync(
            async () =>
            {
                var address = await _context.Addresses.FindAsync(id) ?? throw new OperationException("Address not found.");
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            },
            "Error occurred while deleting the address"
        );
    }

    public async Task<List<Address>> GetAddressesByEntityIdAsync(string entityId)
    {
        return await _context.Addresses
            .Where(d => d.EntityID == entityId)
            .ToListAsync();
    }
}
