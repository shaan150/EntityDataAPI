using EntityDataAPI.Models.Entity;
using EntityDataAPI.DTOs;
using static EntityDataAPI.Utils.UtilityHelpers;

namespace EntityDataAPI.Utils.DTOMappers;


public static class NameDTOMapper
{
    private const string _prefix = "nm";
    public static NameDetailDTO ToDetailDTO(Name name) => new()
    {
        NameID = name.NameID,
        FirstName = name.FirstName,
        MiddleName = name.MiddleName,
        Surname = name.Surname,
        EntityId = name.EntityID
    };

    public static Name ToEntity(NameCreateDTO nameCreateDTO, Entity entity) => new()
    {
        NameID = GenerateNewGuid(_prefix),
        FirstName = nameCreateDTO.FirstName,
        MiddleName = nameCreateDTO.MiddleName,
        Surname = nameCreateDTO.Surname,
        EntityID = nameCreateDTO.EntityID,
        Entity = entity
    };

}
