
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(CategoryCreateDTO categoryDTO);
        Task<bool> UpdateCategoryAsync(Guid id, CategoryUpdateDTO categoryDTO);
        Task<CategoryReadDTO> GetCategoryByIdAsync(Guid categoryId);
        Task<IEnumerable<CategoryReadDTO>> GetAllCategoriesAsync();
        Task<bool> DeleteCategoryAsync(Guid categoryId);
    }
}
