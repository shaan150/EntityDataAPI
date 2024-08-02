using EntityDataAPI.DTOs;
using EntityDataAPI.Enums;
using EntityDataAPI.Exceptions.CRUD;
using EntityDataAPI.Filters;
using EntityDataAPI.Repos.Entity;
using EntityDataAPI.Utils.DTOMappers;
using Microsoft.EntityFrameworkCore;

namespace EntityDataAPI.Controllers.Address
{
    public static class AddressControllerReceiver
    {
        private static TypesEnum _type = TypesEnum.Entity;
        public static async Task<List<AddressDetailDTO>> GetAddresses(IAddressRepo repo, AddressFilter filter)
        {
            var query = repo.GetAllAsQueryable();
            query = filter.ApplyFilters(query);

            var addresses = await query.ToListAsync();

            List<AddressDetailDTO> addressDTOs = addresses
                .Select(AddressDTOMapper.ToDetailDTO)
                .ToList();

            return addressDTOs;
        }

        public static async Task<AddressDetailDTO> GetAddress(IAddressRepo repo, string id)
        {
            var address = await repo.GetByIdAsync(id) ?? throw new NotFoundException(_type, id);
            return AddressDTOMapper.ToDetailDTO(address);
        }
    }
}
