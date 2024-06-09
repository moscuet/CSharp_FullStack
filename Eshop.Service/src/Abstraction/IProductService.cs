using Eshop.Service.src.DTO;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IProductService : IBaseService<ProductCreateDTO, ProductUpdateDTO, ProductReadDTO>
    {
        Task<ProductQueryReadDTO> GetAllProductsAsync(QueryOptions? options);
        Task<Product> ProductCreateAsync(ProductCreateDTO productCreateDto);
        Task<Product> UpdateProductWithImagesAsync(Guid id, ProductUpdateDTO productUpdateDto);
    }
}
