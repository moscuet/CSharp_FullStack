using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IProductSizeService : IBaseService<ProductSizeCreateDTO, ProductSizeUpdateDTO, ProductSizeReadDTO>
    {
        Task<IEnumerable<ProductSizeReadDTO>> GetAllAsync();
    }
}
