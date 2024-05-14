using Eshop.Service.src.DTO;
using Eshop.Core.src.Common;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IProductService
    {
        Task<ProductReadDTO> CreateProductAsync(ProductCreateDTO product);
        Task<bool> UpdateProductByIdAsync(Guid id,ProductUpdateDTO product);
        Task<ProductReadDTO> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductReadDTO>> GetAllProductsAsync(QueryOptions? options);
        Task<bool> DeleteProductByIdAsync(Guid id);
    }
}
