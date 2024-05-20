using AutoMapper;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;

namespace Eshop.Service.src.Service
{
    public class AddressService : BaseService<Address, AddressCreateDTO, AddressUpdateDTO, AddressReadDTO>, IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
            : base(addressRepository, mapper)
        {
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<AddressReadDTO>> GetAllUserAddressesAsync(Guid userId)
        {
            var addresses = await _addressRepository.GetAllUserAddressesAsync(userId);
            return _mapper.Map<IEnumerable<AddressReadDTO>>(addresses);
        }
    }
}
