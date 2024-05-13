

namespace Eshop.Core.src.RepositoryAbstraction
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id);
    }
}