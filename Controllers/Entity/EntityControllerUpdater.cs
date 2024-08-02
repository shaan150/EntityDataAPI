using EntityDataAPI.DTOs;
using EntityDataAPI.Enums;
using EntityDataAPI.Exceptions.CRUD;
using EntityDataAPI.Repos.Entity;

namespace EntityDataAPI.Controllers.Entity;


public static class EntityControllerUpdater
{
    private static TypesEnum _type = TypesEnum.Entity;

    public async static Task UpdateEntity(IEntityRepo repo, string id, EntityUpdateDTO entityUpdateDTO)
    {
        var entity = await repo.GetByIdAsync(id) ?? throw new NotFoundException(_type, id);

        entity.Deceased = entityUpdateDTO.Deceased;
        entity.Gender = entityUpdateDTO?.Gender;
        
        await repo.UpdateAsync(entity);
    }
}
