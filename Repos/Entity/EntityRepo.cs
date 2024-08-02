using EntityDataAPI.Data;
using Microsoft.EntityFrameworkCore;
using static EntityDataAPI.Utils.UtilityHelpers;
using EntityDataAPI.Exceptions;

namespace EntityDataAPI.Repos.Entity;

using EntityDataAPI.DTOs;
using EntityDataAPI.Exceptions.Operations;
using EntityDataAPI.Filters;
using EntityDataAPI.Models.Entity;


public class EntityRepo : IEntityRepo
{
    private readonly AppDbContext _context;
    private readonly IAddressRepo _addressRepo;
    private readonly IDateRepo _dateRepo;
    private readonly INameRepo _nameRepo;

    public EntityRepo(AppDbContext context,
                      IAddressRepo addressRepo,
                      IDateRepo dateRepo,
                      INameRepo nameRepo)
    {
        _context = context;
        _addressRepo = addressRepo;
        _dateRepo = dateRepo;
        _nameRepo = nameRepo;
    }

    public async Task<List<Entity>> GetAllAsync()
    {
        return await _context.Entities
                             .Include(e => e.Addresses)
                             .Include(e => e.Dates)
                             .Include(e => e.Names)
                             .ToListAsync();
    }

    public IQueryable<Entity> GetAllAsQueryable()
    {
        return _context.Entities
                       .Include(e => e.Addresses)
                        .Include(e => e.Dates)
                        .Include(e => e.Names)
                        .AsQueryable();
    }

    public async Task<Entity?> GetByIdAsync(string id)
    {
        return await _context.Entities
                             .Include(e => e.Addresses)
                             .Include(e => e.Dates)
                             .Include(e => e.Names)
                             .FirstOrDefaultAsync(e => e.EntityID == id);
    }

    public async Task<List<Entity?>> GetByIdsAsync(HashSet<string> ids, SearchFilter searchFilter)
    {
        var entitiesQuery = _context.Entities
            .Include(e => e.Addresses)
            .Include(e => e.Dates)
            .Include(e => e.Names)
            .Where(e => ids.Contains(e.EntityID))
            .AsQueryable();

        // Apply additional filters
        var entities = searchFilter.ApplyFilters(entitiesQuery)
            .ToList();

        return entities;
    }

    public async Task<HashSet<string>> SearchAsync(SearchFilter searchFilter)
    {
        string query = searchFilter.SearchQuery ?? string.Empty;
        // Fetch matching addresses and names
        List<Address> addresses = await _addressRepo.SearchAsync(query);
        List<Name> names = await _nameRepo.SearchAsync(query);

        // If no matching addresses or names, return an empty list
        if (addresses.Count == 0 && names.Count == 0)
        {
            return new HashSet<string>();
        }

        // Create a set of EntityIDs from addresses and names
        var entityIds = new HashSet<string>(addresses.Select(a => a.EntityID).Concat(names.Select(n => n.EntityID)));

        // Fetch the relevant entities based on the EntityIDs from addresses and names
        var entities = _context.Entities
            .Where(e => entityIds.Contains(e.EntityID))
            .AsNoTracking()
            .AsQueryable();

        // Apply additional filters
        var entitiesDict = searchFilter.ApplyFilters(entities)
            .ToDictionary(e => e.EntityID);



        return new HashSet<string>(entitiesDict.Keys);
        
    }

    public async Task AddAsync(Entity entity)
    {
        await HandleOperationAsync(
                async () =>
                {
                    await _context.Entities.AddAsync(entity);
                    await _context.SaveChangesAsync();
                },
                "Error occurred while adding the entity"
            );
    }

    public async Task UpdateAsync(Entity entity)
    {
        await HandleOperationAsync(
                async () =>
                {
                    var existingEntity = await _context.Entities.FindAsync(entity.EntityID) ?? throw new OperationException("Entity not found.");

                    _context.Entities.Update(entity);
                    await _context.SaveChangesAsync();
                },
                "Error occurred while updating the entity"
            );
    }

    public async Task DeleteAsync(string id)
    {
        await HandleOperationAsync(
            async () =>
            {
                var entity = await _context.Entities.FindAsync(id) ?? throw new OperationException("Entity not found.");
                _context.Entities.Remove(entity);
                await _context.SaveChangesAsync();
            },
            "Error occurred while deleting the entity"
        );
    }
}
