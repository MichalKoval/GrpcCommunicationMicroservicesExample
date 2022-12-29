using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Data.Entities;
using System.Data;

namespace OrderMicroservice.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAsync(IEnumerable<string> orderIds)
        {
            return await _context.Orders
                .Where(o => !o.IsDeleted && orderIds.Contains(o.Id))
                .Include(o => o.OrderItems)
                .ToListAsync(); ;
        }
        public async Task<Order> AddAsync(Order order)
        {
            order.Id = Guid.NewGuid().ToString();

            var existingOrder = (await GetAsync(new List<string> { order.Id })).FirstOrDefault();

            if (existingOrder is not null)
            {
                throw new DataException($"Add Order: Order with the given Id: {order.Id} already exists.");
            }

            var result = await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
