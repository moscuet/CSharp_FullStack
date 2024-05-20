using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IProductColorService : IBaseService<ProductColorCreateDTO, ProductColorUpdateDTO, ProductColorReadDTO>
    {
        Task<IEnumerable<ProductColorReadDTO>> GetAllAsync();
    }
}
