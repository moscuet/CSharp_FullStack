using Eshop.Core.src.Common;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IAddressService
    {
        Task<AddressReadDTO> CreateAddressAsync(AddressCreateDTO address);
        Task<bool> UpdateAddressByIdAsync(Guid id, AddressUpdateDTO address);
        Task<AddressReadDTO> GetAddressByIdAsync(Guid id);
        Task<IEnumerable<AddressReadDTO>> GetAllUserAddressesAsync(Guid userId);
        Task<bool> DeleteAddressByIdAsync(Guid id);
    }
}
