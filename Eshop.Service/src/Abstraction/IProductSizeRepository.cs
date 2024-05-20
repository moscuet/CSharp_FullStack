using Eshop.Core.src.Entity;

namespace Eshop.Core.src.RepositoryAbstraction
{
    public interface IProductSizeRepository : IBaseRepository<ProductSize>
    {
        Task<IEnumerable<ProductSize>> GetAllAsync();
    }
}
