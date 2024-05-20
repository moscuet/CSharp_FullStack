using Eshop.Core.src.Common;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IProductLineService : IBaseService<ProductLineCreateDTO, ProductLineUpdateDTO, ProductLineReadDTO>
    {
        Task<IEnumerable<ProductLineReadDTO>> GetAllProductLinesAsync(QueryOptions options);
    }
}
