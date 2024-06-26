using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;

namespace Eshop.Core.src.RepositoryAbstraction
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> CreateWithImagesAsync(Product product, List<string> imageUrls);
        Task<Product> UpdateWithImageAsync( Product product, List<string> imageUrls);
        Task<(IEnumerable<Product> Products, int TotalPageNumber)> GetAllProductsAsync(QueryOptions options);
    }
}