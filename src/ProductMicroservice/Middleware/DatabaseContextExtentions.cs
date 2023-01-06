using ProductMicroservice.Data;

namespace ProductMicroservice.Middleware;

public static class DatabaseContextExtentions
{
    public static WebApplication EnsureDbCreated(this WebApplication app)
    {
        try
        {
            using (var scope = app.Services.CreateScope())
            {
                var productDbContext = scope.ServiceProvider.GetService<ProductContext>();

                if (productDbContext is not null)
                {
                    productDbContext.Database.EnsureCreated();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }

        return app;
    }
}