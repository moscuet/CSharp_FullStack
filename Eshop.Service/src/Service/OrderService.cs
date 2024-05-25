using AutoMapper;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;

namespace Eshop.Service.src.Service
{
    public class OrderService : BaseService<Order, OrderCreateDTO, OrderUpdateDTO, OrderReadDTO>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
            : base(orderRepository, mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<OrderReadDTO>> GetAllUserOrdersAsync(Guid userId, QueryOptions? options)
        {
            var orders = await _orderRepository.GetAllUserOrdersAsync(userId, options);
            return _mapper.Map<IEnumerable<OrderReadDTO>>(orders);
        }

        public override async Task<OrderReadDTO> CreateAsync(OrderCreateDTO createDto)
        {
            var order = new Order
            {
                UserId = createDto.UserId,
                AddressId = createDto.AddressId,
                Items = new List<OrderItem>()
            };

            foreach (var itemDto in createDto.Items)
            {
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
                if (product is null)
                {
                    throw new Exception($"Product with ID {itemDto.ProductId} not found.");
                }

                var productLine = product.ProductLine;
                if (productLine is null)
                {
                    throw new Exception($"ProductLine for Product with ID {itemDto.ProductId} not found.");
                }

                var orderItem = new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Price = productLine.Price 
                };

                order.AddItem(orderItem);
            }

            order.RecalculateTotal();
            var createdOrder = await _orderRepository.CreateAsync(order);
            return _mapper.Map<OrderReadDTO>(createdOrder);
        }
    }
}
