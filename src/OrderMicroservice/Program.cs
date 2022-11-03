using OrderMicroservice.Data;
using OrderMicroservice.Data.Repositories;
using OrderMicroservice.Middleware;
using OrderMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderContext>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.EnsureDbCreated();
app.MapGrpcService<OrderService>();

app.Run();