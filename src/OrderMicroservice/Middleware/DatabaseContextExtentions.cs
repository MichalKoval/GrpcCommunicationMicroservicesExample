using OrderMicroservice.Data;

namespace OrderMicroservice.Middleware
{
    public static class DatabaseContextExtentions
    {
        public static WebApplication EnsureDbCreated(this WebApplication app)
        {
            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var orderDbContext = scope.ServiceProvider.GetService<OrderContext>();

                    if (orderDbContext is not null)
                    {
                        orderDbContext.Database.EnsureCreated();
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
}
