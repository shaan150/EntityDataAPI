using EntityDataAPI.DTOs;
using EntityDataAPI.Enums;
using EntityDataAPI.Repos.Entity;
using EntityDataAPI.Utils.DTOMappers;


namespace EntityDataAPI.Controllers.Entity;

using EntityDataAPI.Exceptions.CRUD;
using EntityDataAPI.Filters;
using EntityDataAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;

public static class EntityControllerReceiver
{
    private static TypesEnum _type = TypesEnum.Entity;
    public static async Task<List<EntityDetailDTO>> GetEntities(IEntityRepo repo, EntityFilter filter)
    {
        IQueryable<Entity> query = repo.GetAllAsQueryable();
        query = filter.ApplyFilters(query);

        List<Entity> entities = await query.ToListAsync();

        List<EntityDetailDTO> entityDTOs = entities
            .Select(EntityDTOMapper.ToDetailDTO)
            .ToList();

        return entityDTOs;
    }

    public static async Task<EntityDetailDTO> GetEntity(IEntityRepo repo, string id)
    {
        var entity = await repo.GetByIdAsync(id) ?? throw new NotFoundException(_type, id);
        return EntityDTOMapper.ToDetailDTO(entity);
    }

    public static async Task<List<EntityDetailDTO>> SearchEntities(IEntityRepo repo, SearchFilter search)
    {
        HashSet<string> entityIds = await SearchEntityIds(repo, search);

        // Fetch all entities in a single query
        List<Entity?> entities = await repo.GetByIdsAsync(entityIds, search);

        // Convert entities to DTOs
        List<EntityDetailDTO> entityDTOs = entities
            .Where(entity => entity != null) // Ensure no null entities
            .Select(EntityDTOMapper.ToDetailDTO)
            .ToList();


        return entityDTOs;
    }

    public static async Task<HashSet<string>> SearchEntityIds(IEntityRepo repo, SearchFilter search)
    {
        if (search.SearchQuery == null)
            throw new MissingSearchQueryException();

        return await repo.SearchAsync(search);
    }

}
