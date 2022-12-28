using Api.Protos;

namespace Api.Services;

public interface IProductOrderService
{
    Task<IEnumerable<OrderDto>> GetOrdersAsync(IEnumerable<string> orderIds);
    Task<IEnumerable<ProductDto>> GetProductsAsync(IEnumerable<string> productIds);
}