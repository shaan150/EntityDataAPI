using EntityDataAPI.DTOs;
using EntityDataAPI.Enums;
using EntityDataAPI.Exceptions.CRUD;
using EntityDataAPI.Repos.Entity;
using EntityDataAPI.Utils.DTOMappers;

namespace EntityDataAPI.Controllers.Address
{
    public static class AddressControllerCreator
    {
        private static TypesEnum _type = TypesEnum.Address;

        public static async Task<string> CreateAddress(IEntityRepo entityRepo, IAddressRepo repo, AddressCreateDTO addressCreateDTO)
        {
            if (addressCreateDTO.EntityID == null)
            {
                throw new ArgumentException("EntityID cannot be null");
            }

            var entity = await entityRepo.GetByIdAsync(addressCreateDTO.EntityID) ?? throw new NotFoundException(TypesEnum.Entity, addressCreateDTO.EntityID);

            var address = AddressDTOMapper.ToEntity(addressCreateDTO, entity);
            await repo.AddAsync(address);

            return address.AddressID;
        }

    }
}
