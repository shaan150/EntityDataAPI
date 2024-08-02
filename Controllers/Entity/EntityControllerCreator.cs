using EntityDataAPI.DTOs;
using EntityDataAPI.Enums;
using EntityDataAPI.Exceptions.Operations;
using EntityDataAPI.Repos.Entity;
using EntityDataAPI.Utils.DTOMappers;

namespace EntityDataAPI.Controllers.Entity;

public static class EntityControllerCreator
{
    private static TypesEnum _type = TypesEnum.Entity;

    public static async Task<string> CreateEntity(IEntityRepo repo, EntityCreateDTO entityCreateDTO)
    {
        var entity = EntityDTOMapper.ToEntity(entityCreateDTO);
        await repo.AddAsync(entity);

        return entity.EntityID;
    }
}
