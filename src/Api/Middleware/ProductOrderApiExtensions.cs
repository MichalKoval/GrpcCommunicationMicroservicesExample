using Api.Dtos;
using Api.Services;
using AutoMapper;

namespace Api.Middleware;

public static class ProductOrderApiExtensions
{
    public static WebApplication MapProductOrderApi(this WebApplication app)
    {
        app.MapGet("/order", async (IMapper mapper, IProductOrderService productOrderService) =>
        {
            var orderIds = new[] {"f6a95866-a7eb-4be3-90b9-fc81ef23f194", "7ae62388-ea9e-4910-a43c-dcf72959dae2"};
            var orders = (await productOrderService.GetOrdersAsync(orderIds)).ToList();
            var ordersResult = orders.Select(o => mapper.Map<OrderResult>(o)).ToList();

            var productIds = orders.SelectMany(o => o.OrderItems.Select(oi => oi.ProductId));
            var products = await productOrderService.GetProductsAsync(productIds);
            var productResults = products.Select(p => mapper.Map<ProductResult>(p));
            var productResultsByIds = productResults.ToDictionary(p => p.Id);

            var ordersResultProducts = ordersResult.SelectMany(o => o.OrderItems.Select(oi => oi.ProductInfo));

            foreach (var product in ordersResultProducts)
            {
                var productResult = productResultsByIds[product.Id];

                product.Name = productResult.Name;
                product.Description = productResult.Description;
                product.Size = productResult.Size;
                product.Reviews = productResult.Reviews;
            }

            return new GetOrdersResult
            {
                Orders = ordersResult
            };
        }).WithName("GetProductOrders");

        return app;
    }
}