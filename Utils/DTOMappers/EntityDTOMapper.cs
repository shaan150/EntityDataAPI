using EntityDataAPI.DTOs;
using EntityDataAPI.Models.Entity;

namespace EntityDataAPI.Utils.DTOMappers;

public static class EntityDTOMapper
{
    private const string _prefix = "ent";

    public static EntityDetailDTO ToDetailDTO(Entity entity) => new()
    {
        EntityID = entity.EntityID,
        Deceased = entity.Deceased,
        Gender = entity.Gender,
        Addresses = entity.Addresses?.Select(a => AddressDTOMapper.ToDetailDTO(a)).ToList() ?? [],
        Dates = entity.Dates?.Select(d => DateDTOMapper.ToDetailDTO(d)).ToList() ?? [],
        Names = entity.Names?.Select(n => NameDTOMapper.ToDetailDTO(n)).ToList() ?? []
    };

    public static Entity ToEntity(EntityCreateDTO entityCreateDTO) => new()
    {
        EntityID = UtilityHelpers.GenerateNewGuid(_prefix), // Generate a new GUID for the entity
        Deceased = entityCreateDTO.Deceased,
        Gender = entityCreateDTO.Gender,
        Addresses = [],
        Dates = [],
        Names = []
    };
}
