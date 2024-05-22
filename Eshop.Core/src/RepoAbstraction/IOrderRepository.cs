using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;

namespace Eshop.Core.src.RepositoryAbstraction
{
  public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllUserOrdersAsync(Guid userId, QueryOptions? options);
    }
}