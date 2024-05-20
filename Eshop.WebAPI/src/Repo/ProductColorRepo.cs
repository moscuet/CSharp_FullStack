using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Repo
{
    public class ProductColorRepository : IProductColorRepository
    {
        private readonly EshopDbContext _context;

        public ProductColorRepository(EshopDbContext context)
        {
            _context = context;
        }

        public async Task<ProductColor> CreateAsync(ProductColor entity)
        {
            await _context.ProductColors.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(ProductColor entity)
        {
            _context.ProductColors.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ProductColor> GetByIdAsync(Guid id)
        {
            return await _context.ProductColors.FindAsync(id);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.ProductColors.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ProductColor>> GetAllAsync()
        {
            return await _context.ProductColors.ToListAsync();
        }
    }
}
