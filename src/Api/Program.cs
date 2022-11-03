using Api.Middleware;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClients(builder.Configuration);
builder.Services.AddScoped<IProductOrderService, ProductOrderService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapProductOrderApi();

app.Run();