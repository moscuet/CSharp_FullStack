using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
   public interface IOrderService : IBaseService<OrderCreateDTO, OrderUpdateDTO, OrderReadDTO>
    {
        Task<IEnumerable<OrderReadDTO>> GetAllUserOrdersAsync(Guid userId, QueryOptions? options);
    }
}
