using OrderMicroservice.Data.Entities;

namespace OrderMicroservice.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAsync(IEnumerable<string> orderIds);
        public Task<Order> AddAsync(Order order);
    }
}
