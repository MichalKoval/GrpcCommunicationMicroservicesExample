using ProductMicroservice.Data;
using ProductMicroservice.Data.Repositories;
using ProductMicroservice.Middleware;
using ProductMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductContext>();
builder.Services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.EnsureDbCreated();
app.MapGrpcService<ProductService>();

app.Run();
