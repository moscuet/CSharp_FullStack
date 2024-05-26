using System.Text.Json;
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
        #pragma warning disable CS8620 
            var productLine = await _productLines
        .Include(pl => pl.Products)
            .ThenInclude(p => p.ProductImages)
        .Include(pl => pl.Products)
            .ThenInclude(p => p.Reviews)
                .ThenInclude(r => r.ReviewImages)
        .FirstOrDefaultAsync(pl => pl.Id == id);
         #pragma warning restore CS8620

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
            var limit = options.Limit.HasValue ? options.Limit.Value.ToString() : "NULL";
            var offset = options.StartingAfter.HasValue ? options.StartingAfter.Value.ToString() : "NULL";
            var sortBy = string.IsNullOrWhiteSpace(options.SortBy?.ToString()) ? "NULL" : $"'{options.SortBy.ToString()}'";
            var sortOrder = string.IsNullOrWhiteSpace(options.SortOrder?.ToString()) ? "NULL" : $"'{options.SortOrder}'";
            var searchKey = string.IsNullOrWhiteSpace(options.SearchKey) ? "NULL" : $"'{options.SearchKey}'";
            var categoryName = string.IsNullOrWhiteSpace(options.CategoryName) ? "NULL" : $"'{options.CategoryName}'";

            var sql = $"SELECT * FROM get_product_lines({limit}, {offset}, {sortBy}, {sortOrder}, {searchKey}, {categoryName})";

            var productLines = await _productLines.FromSqlRaw(sql)
                .ToListAsync();

            Console.WriteLine($"From review controller: reviewDto: {JsonSerializer.Serialize(productLines)}");
            return productLines;
        }
    }

}
