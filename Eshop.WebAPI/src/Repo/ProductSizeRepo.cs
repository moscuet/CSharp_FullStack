using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Repo
{
    public class ProductSizeRepository : IProductSizeRepository
    {
        private readonly EshopDbContext _context;

        public ProductSizeRepository(EshopDbContext context)
        {
            _context = context;
        }

        public async Task<ProductSize> CreateAsync(ProductSize entity)
        {
            await _context.ProductSizes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(ProductSize entity)
        {
            _context.ProductSizes.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ProductSize> GetByIdAsync(Guid id)
        {
            return await _context.ProductSizes.FindAsync(id);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.ProductSizes.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ProductSize>> GetAllAsync()
        {
            return await _context.ProductSizes.ToListAsync();
        }
    }
}
