using Eshop.Core.src.Entity;

namespace Eshop.Core.src.RepositoryAbstraction
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> FindByNameAsync(string name);
        Task<IEnumerable<Category>> GetAllAsync();
    }
}