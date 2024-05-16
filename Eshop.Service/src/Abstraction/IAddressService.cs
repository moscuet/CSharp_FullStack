using Eshop.Core.src.Common;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IAddressService
    {
        public interface IAddressService : IBaseService<AddressCreateDTO, AddressUpdateDTO, AddressReadDTO>
    {
        Task<IEnumerable<AddressReadDTO>> GetAllUserAddressesAsync(Guid userId);
    }  }
}
