using EntityDataAPI.Data;
using EntityDataAPI.Models.Entity;
using EntityDataAPI.Exceptions.Operations;
using static EntityDataAPI.Utils.UtilityHelpers;
using Microsoft.EntityFrameworkCore;

namespace EntityDataAPI.Repos.Entity;

public class NameRepo : INameRepo
{
    private readonly AppDbContext _context;

    public NameRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Name>> GetAllAsync()
    {
        return await _context.Names.ToListAsync();
    }

    public async Task<List<Name>> SearchAsync(string query)
    {
        return await _context.Names
                            .Where(n => EF.Functions.Like(n.FirstName, $"%{query}%") ||
                                EF.Functions.Like(n.MiddleName, $"%{query}%") ||
                                EF.Functions.Like(n.Surname, $"%{query}%"))
                            .ToListAsync();
    }

    public async Task<Name?> GetByIdAsync(string id)
    {
        return await _context.Names.FindAsync(id);
    }

    public async Task AddAsync(Name name)
    {
        await HandleOperationAsync(
            async () =>
            {
                await _context.Names.AddAsync(name);
                await _context.SaveChangesAsync();
            },
            "Error occurred while adding the name"
        );
    }

    public async Task UpdateAsync(Name name)
    {
        await HandleOperationAsync(
            async () =>
            {
                var existingName = await _context.Names.FindAsync(name.NameID) ?? throw new OperationException("Name not found.");
                _context.Names.Update(name);
                await _context.SaveChangesAsync();
            },
            "Error occurred while updating the name"
        );
    }

    public async Task DeleteAsync(string id)
    {
        await HandleOperationAsync(
            async () =>
            {
                var name = await _context.Names.FindAsync(id) ?? throw new OperationException("Name not found.");
                _context.Names.Remove(name);
                await _context.SaveChangesAsync();
            },
            "Error occurred while deleting the name"
        );
    }

    public async Task<List<Name>> GetNamesByEntityIdAsync(string entityId)
        {
            return await _context.Names
                .Where(n => n.EntityID == entityId)
                .ToListAsync();
        }
}
