using AutoMapper;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;

namespace Eshop.Service.src.Service
{
    public class ProductLineService : BaseService<ProductLine, ProductLineCreateDTO, ProductLineUpdateDTO, ProductLineReadDTO>, IProductLineService
    {
        private readonly IProductLineRepository _productLineRepository;

        public ProductLineService(IProductLineRepository productLineRepository, IMapper mapper)
            : base(productLineRepository, mapper)
        {
            _productLineRepository = productLineRepository;
        }

        public async Task<IEnumerable<ProductLineReadDTO>> GetAllProductLinesAsync(QueryOptions options)
        {
            var productLines = await _productLineRepository.GetAllProductLinesAsync(options);
            return _mapper.Map<IEnumerable<ProductLineReadDTO>>(productLines);
        }
    }
}

