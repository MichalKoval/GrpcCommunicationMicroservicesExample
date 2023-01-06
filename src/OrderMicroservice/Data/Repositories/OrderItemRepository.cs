using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Data.Entities;
using System.Data;

namespace OrderMicroservice.Data.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly OrderContext _context;

    public OrderItemRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task<OrderItem> AddAsync(OrderItem orderItem)
    {
        orderItem.Id = Guid.NewGuid().ToString();

        var existingOrderItem = await GetAsync(orderItem.Id);

        if (existingOrderItem is not null)
        {
            throw new DataException($"Add OrderItem: OrderItem with the given Id: {orderItem.Id} already exists.");
        }

        var result = await _context.OrderItems.AddAsync(orderItem);
            
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    private async Task<OrderItem?> GetAsync(string orderItemId)
    {
        return await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == orderItemId && !o.IsDeleted);
    }
}