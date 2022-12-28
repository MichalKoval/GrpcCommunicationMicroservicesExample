using Api.Protos;
using Grpc.Core;

namespace Api.Services;

public class ProductOrderService : IProductOrderService
{
    private readonly ILogger<ProductOrderService> _logger;
    private readonly OrderServiceProto.OrderServiceProtoClient _orderClient;
    private readonly ProductServiceProto.ProductServiceProtoClient _productClient;

    public ProductOrderService(
        ILogger<ProductOrderService> logger,
        OrderServiceProto.OrderServiceProtoClient orderClient,
        ProductServiceProto.ProductServiceProtoClient productClient)
    {
        _logger = logger;
        _orderClient = orderClient;
        _productClient = productClient;
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersAsync(IEnumerable<string> orderIds)
    {
        try
        {
            _logger.LogInformation("Calling Order microservice method: GetOrders().");
            
            var ordersRequest = new GetOrdersRequest();
            ordersRequest.OrderIds.AddRange(orderIds);

            var orders = new List<OrderDto>();
            using var ordersCall = _orderClient.GetOrders(ordersRequest);

            await foreach (var order in ordersCall.ResponseStream.ReadAllAsync())
            {
                orders.Add(order);
            }

            return orders;
        }
        catch (Exception e)
        {
            _logger.LogWarning(e.Message);
            return new List<OrderDto>();
        }
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync(IEnumerable<string> productIds)
    {
        try
        {
            _logger.LogInformation("Calling Product microservice method: GetProducts().");

            var productsRequest = new GetProductsRequest();
            productsRequest.ProductIds.AddRange(productIds);

            var products = new List<ProductDto>();
            using var productsCall = _productClient.GetProducts(productsRequest);

            await foreach (var product in productsCall.ResponseStream.ReadAllAsync())
            {
                products.Add(product);
            }

            return products;
        }
        catch (Exception e)
        {
            _logger.LogWarning(e.Message);
            return new List<ProductDto>();
        }
    }
}