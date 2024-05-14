using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(OrderCreateDTO order);
        Task<bool> UpdateOrderByIdAsync(Guid id, OrderUpdateDTO order);
        Task<Order>? GetOrderByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllUserOrdersAsync(Guid userId, QueryOptions? options);
        Task<bool> DeleteOrderByIdAsync(Guid id);
    }
}
