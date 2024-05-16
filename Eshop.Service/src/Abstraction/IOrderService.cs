using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IOrderService : IBaseService<OrderCreateDTO, OrderUpdateDTO, Order>
    {
        Task<IEnumerable<Order>> GetAllUserOrdersAsync(Guid userId, QueryOptions? options);
    }
}
