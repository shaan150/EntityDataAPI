using EntityDataAPI.Data;
using EntityDataAPI.Models.Entity;
using EntityDataAPI.Exceptions.Operations;
using Microsoft.EntityFrameworkCore;
using static EntityDataAPI.Utils.UtilityHelpers;


namespace EntityDataAPI.Repos.Entity;

public class DateRepo : IDateRepo
{
    private readonly AppDbContext _context;

    public DateRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Date>> GetAllAsync()
    {
        return await _context.Dates.ToListAsync();
    }

    public async Task<Date?> GetByIdAsync(string id)
    {
        return await _context.Dates.FindAsync(id);
    }

    public async Task AddAsync(Date date)
    {
        await HandleOperationAsync(
            async () =>
            {
                await _context.Dates.AddAsync(date);
                await _context.SaveChangesAsync();
            },
            "Error occurred while adding the date"
        );
    }

    public async Task UpdateAsync(Date date)
    {
        await HandleOperationAsync(
            async () =>
            {
                var existingDate = await _context.Dates.FindAsync(date.DateID) ?? throw new OperationException("Date not found.");
                _context.Dates.Update(date);
                await _context.SaveChangesAsync();
            },
            "Error occurred while updating the date"
        );
    }

    public async Task DeleteAsync(string id)
    {
        await HandleOperationAsync(
            async () =>
            {
                var date = await _context.Dates.FindAsync(id) ?? throw new OperationException("Date not found.");
                _context.Dates.Remove(date);
                await _context.SaveChangesAsync();
            },
            "Error occurred while deleting the date"
        );
    }

    public async Task<List<Date>> GetDatesByEntityIdAsync(string entityId)
    {
        return await _context.Dates
            .Where(d => d.EntityId == entityId)
            .ToListAsync();
    }
}
