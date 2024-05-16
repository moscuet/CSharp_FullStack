
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface ICategoryService : IBaseService<CategoryCreateDTO, CategoryUpdateDTO, CategoryReadDTO>
    {
        Task<IEnumerable<CategoryReadDTO>> GetAllCategoriesAsync();
    }
}
