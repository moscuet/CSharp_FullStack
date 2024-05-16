using Eshop.Service.src.DTO;
using Eshop.Core.src.Common;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IProductService : IBaseService<ProductCreateDTO, ProductUpdateDTO, ProductReadDTO>
    {
        Task<IEnumerable<ProductReadDTO>> GetAllProductsAsync(QueryOptions? options);
    }
}
