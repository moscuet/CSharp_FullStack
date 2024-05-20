using AutoMapper;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;


namespace Eshop.Service.src.Service
{
    public class CategoryService : BaseService<Category, CategoryCreateDTO, CategoryUpdateDTO, CategoryReadDTO>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
            : base(categoryRepository, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<CategoryReadDTO> CreateAsync(CategoryCreateDTO categoryDTO)
        {
            if (categoryDTO.ParentCategoryId.HasValue)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(categoryDTO.ParentCategoryId.Value);
                if (parentCategory == null)
                {
                    throw new KeyNotFoundException($"Parent category with ID {categoryDTO.ParentCategoryId.Value} not found.");
                }
            }

            return await base.CreateAsync(categoryDTO);
        }

        public override async Task<bool> UpdateAsync(Guid id, CategoryUpdateDTO categoryDTO)
        {
            if (categoryDTO.ParentCategoryId.HasValue)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(categoryDTO.ParentCategoryId.Value);
                if (parentCategory == null)
                {
                    throw new KeyNotFoundException($"Parent category with ID {categoryDTO.ParentCategoryId.Value} not found.");
                }
            }

            return await base.UpdateAsync(id, categoryDTO);
        }

        public async Task<IEnumerable<CategoryReadDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryReadDTO>>(categories);
        }

        public async Task<CategoryReadDTO> FindByNameAsync(string name)
        {
            var category = await _categoryRepository.FindByNameAsync(name);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with name {name} not found.");
            }
            return _mapper.Map<CategoryReadDTO>(category);
        }
    }
}
