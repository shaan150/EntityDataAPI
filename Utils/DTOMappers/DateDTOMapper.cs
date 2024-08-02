using EntityDataAPI.DTOs;
using EntityDataAPI.Models.Entity;
using static EntityDataAPI.Utils.UtilityHelpers;

namespace EntityDataAPI.Utils.DTOMappers;

public static class DateDTOMapper
{
    private const string _prefix = "dt";

    public static DateDetailDTO ToDetailDTO(Date Date) => new()
    {
        DateID = Date.DateID,
        DateType = Date.DateType,
        DateTime = Date.DateTime,
        EntityId = Date.EntityId
    };

    public static Date ToEntity(DateCreateDTO DateCreateDTO, Entity entity) => new()
    {
        DateID = GenerateNewGuid(_prefix),
        DateType = DateCreateDTO.DateType,
        DateTime = DateCreateDTO.DateTime,
        EntityId = DateCreateDTO.EntityID,
        Entity = entity
    };
}
