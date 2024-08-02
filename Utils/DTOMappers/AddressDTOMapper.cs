using EntityDataAPI.DTOs;
using EntityDataAPI.Models.Entity;
using static EntityDataAPI.Utils.UtilityHelpers;

namespace EntityDataAPI.Utils.DTOMappers;

public static class AddressDTOMapper
{
    private const string _prefix = "adr";

    public static AddressDetailDTO ToDetailDTO(Address address) => new()
    {
        AddressId = address.AddressID,
        AddressLine = address.AddressLine,
        City = address.City,
        Country = address.Country,
        EntityId = address.EntityID
    };

    public static Address ToEntity(AddressCreateDTO addressCreateDTO, Entity entity) => new()
    {
        AddressID = GenerateNewGuid(_prefix),
        AddressLine = addressCreateDTO.AddressLine,
        City = addressCreateDTO.City,
        Country = addressCreateDTO.Country,
        EntityID = addressCreateDTO.EntityID,
        Entity = entity
    };
}
