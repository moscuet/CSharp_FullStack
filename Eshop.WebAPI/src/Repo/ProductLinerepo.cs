using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Repo
{
    public class ProductLineRepository : IProductLineRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<ProductLine> _productLines;

        public ProductLineRepository(EshopDbContext context)
        {
            _context = context;
            _productLines = _context.ProductLines;
        }

        public async Task<ProductLine> CreateAsync(ProductLine productLine)
        {
            await _productLines.AddAsync(productLine);
            await _context.SaveChangesAsync();
            return productLine;
        }

        public async Task<bool> UpdateAsync(ProductLine productLine)
        {
            var existingProductLine = await _productLines.AsNoTracking().FirstOrDefaultAsync(pl => pl.Id == productLine.Id);
            if (existingProductLine == null)
            {
                throw new KeyNotFoundException($"ProductLine with ID {productLine.Id} not found.");
            }

            _context.Update(productLine);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductLine> GetByIdAsync(Guid id)
        {
            var productLine = await _productLines.FirstOrDefaultAsync(pl => pl.Id == id);
            if (productLine == null)
            {
                throw new KeyNotFoundException($"ProductLine with ID {id} not found.");
            }
            return productLine;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var productLine = await _productLines.FirstOrDefaultAsync(pl => pl.Id == id);
            if (productLine == null)
            {
                throw new KeyNotFoundException($"ProductLine with ID {id} not found.");
            }

            _productLines.Remove(productLine);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductLine>> GetAllProductLinesAsync(QueryOptions options)
        {
            return await _productLines.ToListAsync();
        }
    }
}
