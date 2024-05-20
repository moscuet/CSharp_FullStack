using AutoMapper;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;

namespace Eshop.Service.src.Service
{
    public class ProductColorService : BaseService<ProductColor, ProductColorCreateDTO, ProductColorUpdateDTO, ProductColorReadDTO>, IProductColorService
    {
        private readonly IProductColorRepository _productColorRepository;

        public ProductColorService(IProductColorRepository productColorRepository, IMapper mapper)
            : base(productColorRepository, mapper)
        {
            _productColorRepository = productColorRepository;
        }

        public async Task<IEnumerable<ProductColorReadDTO>> GetAllAsync()
        {
            var entities = await _productColorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductColorReadDTO>>(entities);
        }
    }
}
