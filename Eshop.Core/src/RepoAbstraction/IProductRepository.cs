using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;

namespace Eshop.Core.src.RepositoryAbstraction
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(QueryOptions options);
    }
}