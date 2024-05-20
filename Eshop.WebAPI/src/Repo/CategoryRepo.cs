using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Repo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<Category> _categories;

        public CategoryRepository(EshopDbContext context)
        {
            _context = context;
            _categories = _context.Categories;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            var existingCategory = await _categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == category.Id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {category.Id} not found.");
            }

            _context.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await _categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return category;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var category = await _categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            _categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categories.ToListAsync();
        }

        public async Task<Category> FindByNameAsync(string name)
        {
            var category = await _categories.FirstOrDefaultAsync(c => c.Name == name);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with name {name} not found.");
            }
            return category;
        }
    }
}
