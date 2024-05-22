using AutoMapper;
using Eshop.Core.src.Common;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;
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
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> GetAllUserOrders(Guid userId, [FromQuery] QueryOptions options)
        {
            var orders = await _orderService.GetAllUserOrdersAsync(userId, options);
            return Ok(_mapper.Map<IEnumerable<OrderReadDTO>>(orders));
        }

        // GET: api/Order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDTO>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderReadDTO>(order));
        }


        // PUT: api/Order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, OrderUpdateDTO orderUpdateDto)
        {
            try
            {
                var result = await _orderService.UpdateAsync(id, orderUpdateDto);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                var result = await _orderService.DeleteByIdAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
