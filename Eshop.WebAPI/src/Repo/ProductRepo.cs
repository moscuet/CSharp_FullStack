using System.Text.Json;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Eshop.WebApi.src.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<Product> _products;
        private readonly DbSet<ProductImage> _images;

        public ProductRepository(EshopDbContext context)
        {
            _context = context;
            _products = _context.Products;
            _images = _context.ProductImages;

        }

        public async Task<Product> CreateAsync(Product product)
        {
            Console.WriteLine("I am here in repo1");
            await _products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> CreateWithImagesAsync(Product product, List<string> imageUrls)
        {

            Console.WriteLine("I am here in repo");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _products.AddAsync(product);
                await _context.SaveChangesAsync();

                if (imageUrls != null && imageUrls.Count > 0)
                {
                    var productImages = imageUrls.Select(imageUrl => new ProductImage
                    {
                        ProductId = product.Id,
                        Url = imageUrl
                    }).ToList();

                    await _images.AddRangeAsync(productImages);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                Console.WriteLine($"From review controller: reviewDto: {JsonSerializer.Serialize(product)}");

                return product;
            }
            catch (Exception ex)
            {
                if (transaction.GetDbTransaction().Connection != null)
                {
                    await transaction.RollbackAsync();
                }
                throw new Exception("An error occurred while creating the product with images.", ex);
            }
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
