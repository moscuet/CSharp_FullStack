using Eshop.Core.src.Entity;

namespace Eshop.Core.src.RepositoryAbstraction
{
    public interface IProductColorRepository : IBaseRepository<ProductColor>
    {
        Task<IEnumerable<ProductColor>> GetAllAsync();
    }
}
