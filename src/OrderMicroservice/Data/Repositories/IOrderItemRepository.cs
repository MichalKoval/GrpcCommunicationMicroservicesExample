using OrderMicroservice.Data.Entities;

namespace OrderMicroservice.Data.Repositories;

public interface IOrderItemRepository
{
    public Task<OrderItem> AddAsync(OrderItem orderItem);
}