using AutoMapper;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;

namespace Eshop.Service.src.Service
{
    public class ProductSizeService : BaseService<ProductSize, ProductSizeCreateDTO, ProductSizeUpdateDTO, ProductSizeReadDTO>, IProductSizeService
    {
        private readonly IProductSizeRepository _productSizeRepository;

        public ProductSizeService(IProductSizeRepository productSizeRepository, IMapper mapper)
            : base(productSizeRepository, mapper)
        {
            _productSizeRepository = productSizeRepository;
        }

        public async Task<IEnumerable<ProductSizeReadDTO>> GetAllAsync()
        {
            var entities = await _productSizeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductSizeReadDTO>>(entities);
        }
    }
}
