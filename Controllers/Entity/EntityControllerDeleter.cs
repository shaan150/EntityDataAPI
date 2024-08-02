using EntityDataAPI.Enums;
using EntityDataAPI.Exceptions.CRUD;
using EntityDataAPI.Repos.Entity;

namespace EntityDataAPI.Controllers.Entity;

public static class EntityControllerDeleter
{
    private static TypesEnum _type = TypesEnum.Entity;
    public static async Task DeleteEntity(string id, IEntityRepo entityRepo)
    {
        var entity = await entityRepo.GetByIdAsync(id) ?? throw new NotFoundException(_type, id);
        await entityRepo.DeleteAsync(id);
    }
}
