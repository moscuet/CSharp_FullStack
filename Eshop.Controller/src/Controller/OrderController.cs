using System.Text.Json;
using AutoMapper;
using Eshop.Core.src.Common;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        // POST: api/Order
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderReadDTO>> CreateAsync([FromBody] OrderCreateControllerDTO orderDto)
        {

            var productIds = new HashSet<Guid>();
            foreach (var item in orderDto.Items)
            {
                if (!productIds.Add(item.ProductId))
                {
                    throw new InvalidOperationException("Duplicate ProductId detected in order items.");
                }
            }

            var (currentUserId, _) = UserContextHelper.GetUserClaims(HttpContext);

            OrderCreateDTO OrderCreateDTO = _mapper.Map<OrderCreateDTO>(orderDto);

            OrderCreateDTO.UserId = currentUserId.Value;

            var createdOrder = await _orderService.CreateAsync(OrderCreateDTO);
            return Ok(createdOrder);
        }


        // GET: api/Order
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> GetAllUserOrders(Guid? userId, [FromQuery] QueryOptions options)
        {
            
            var (currentUserId, currentUserRole) = UserContextHelper.GetUserClaims(HttpContext);
            
          Console.WriteLine($"currentUserId: {currentUserId}");
         Console.WriteLine($"currentUserRole: {currentUserRole}");

            if (userId.HasValue && currentUserRole != "Admin"){
                Console.WriteLine("User is not an Admin, so he can only see his own orders");
             return Forbid();
            }
            Guid fetchUserId = (Guid)(userId ?? currentUserId);

            var orders = await _orderService.GetAllUserOrdersAsync(fetchUserId, options);
           
            Console.WriteLine(JsonSerializer.Serialize(orders));

            return Ok(_mapper.Map<IEnumerable<OrderReadDTO>>(orders));
        }


        // GET: api/Order/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderReadDTO>> GetOrderById(Guid id)
        {

            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            var (currentUserId, currentUserRole) = UserContextHelper.GetUserClaims(HttpContext);
            if (currentUserId != order.UserId && currentUserRole != "Admin")
                return Forbid();

            return Ok(_mapper.Map<OrderReadDTO>(order));
        }

        // PUT: api/Order/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOrder(Guid id, OrderUpdateDTO orderUpdateDto)
        {
            var (currentUserId, currentUserRole) = UserContextHelper.GetUserClaims(HttpContext);
            var order = await _orderService.GetByIdAsync(id);
            if (currentUserId != order.UserId && currentUserRole != "Admin")
                return Forbid();

            await _orderService.UpdateAsync(id, orderUpdateDto);

            return NoContent();
        }

        // DELETE: api/Order/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var (currentUserId, currentUserRole) = UserContextHelper.GetUserClaims(HttpContext);
            var order = await _orderService.GetByIdAsync(id);
            if (currentUserId != order.UserId && currentUserRole != "Admin")
                return Forbid();

            await _orderService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
