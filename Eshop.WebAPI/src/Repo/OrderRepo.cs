using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Core.src.ValueObject;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Repo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<Product> _products;
        private readonly DbSet<Order> _orders;

        public OrderRepository(EshopDbContext context)
        {
            _context = context;
            _products = _context.Products;
            _orders = _orders;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var item in order.Items)
                {
                    var product = await _products.FindAsync(item.ProductId);
                    if (product == null)
                    {
                        throw new Exception($"Product with ID {item.ProductId} not found.");
                    }

                    if (product.Inventory < item.Quantity)
                    {
                        throw new Exception($"Not enough inventory for product with ID {item.ProductId}.");
                    }

                    product.Inventory -= item.Quantity;
                    _products.Update(product);
                }

                _orders.Add(order);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return order;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }



        public async Task<bool> UpdateAsync(Order order)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingOrder = await _orders.Include(o => o.Items)
                                                         .FirstOrDefaultAsync(o => o.Id == order.Id);

                if (existingOrder == null)
                {
                    throw new KeyNotFoundException("Order not found");
                }

                // Update order properties
                existingOrder.AddressId = order.AddressId;
                existingOrder.Status = order.Status;
                existingOrder.Items = order.Items; 
                existingOrder.RecalculateTotal(); 

                _orders.Update(existingOrder);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _orders.Include(o => o.Items)
                                        .ThenInclude(i => i.Product)
                                        .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var order = await _orders.FindAsync(id);
            if (order == null )
            {
                return false;
            }
            _orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllUserOrdersAsync(Guid userId, QueryOptions? options)
        {
            var query = _orders.Include(o => o.Items)
                                       .ThenInclude(i => i.Product)
                                       .Where(o => o.UserId == userId);

            if (options?.SortBy == SortBy.Date)
            {
                query = options.SortOrder == SortOrder.Descending ?
                        query.OrderByDescending(o => o.CreatedAt) :
                        query.OrderBy(o => o.CreatedAt);
            }

            if (options?.Limit != null)
            {
                query = query.Take(options.Limit.Value);
            }

            return await query.ToListAsync();
        }
    }
}
