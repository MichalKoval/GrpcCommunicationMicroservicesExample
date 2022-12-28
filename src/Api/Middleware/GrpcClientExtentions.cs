using Api.Protos;

namespace Api.Middleware;

public static class GrpcClientExtentions
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<OrderServiceProto.OrderServiceProtoClient>(c =>
        {
            c.Address = new Uri(configuration.GetValue<string>("Microservices:OrderMicroserviceUrl"));
        });
        services.AddGrpcClient<ProductServiceProto.ProductServiceProtoClient>(c =>
        {
            c.Address = new Uri(configuration.GetValue<string>("Microservices:ProductMicroserviceUrl"));
        });

        return services;
    }
}