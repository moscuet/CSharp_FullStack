using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<Product> _products;

        public ProductRepository(EshopDbContext context)
        {
            _context = context;
            _products = _context.Products;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var existingProduct = await _products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.Id} not found.");
            }

            _context.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await _products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return product;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var product = await _products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            _products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(QueryOptions options)
        {
            return await _products.ToListAsync();
        }
    }
}
