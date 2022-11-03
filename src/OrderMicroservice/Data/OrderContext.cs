using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Data.Entities;

namespace OrderMicroservice.Data
{
    public class OrderContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "OrderDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region OrderSeed
            var order1 = new Order
            {
                Id = "f6a95866-a7eb-4be3-90b9-fc81ef23f194"
            };
            var order2 = new Order
            {
                Id = "7ae62388-ea9e-4910-a43c-dcf72959dae2"
            };

            modelBuilder.Entity<Order>().HasData(new List<Order> { order1, order2 });
            #endregion

            #region OrderItemSeed
            var orderItem1 = new OrderItem
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = "1bebc704-c7f6-4f77-beed-52ed08ed0716",
                OrderId = order1.Id,
                Quantity = 1,
                Price = 5,
                PromotionCode = "",
                ExtendedGurantee = false
            };
            var orderItem2 = new OrderItem
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = "c126f115-4905-448e-a00f-c33a3c79a8e5",
                OrderId = order2.Id,
                Quantity = 2,
                Price = 10,
                PromotionCode = "",
                ExtendedGurantee = false
            };
            var orderItem3 = new OrderItem
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = "c126f115-4905-448e-a00f-c33a3c79a8e5",
                OrderId = order1.Id,
                Quantity = 3,
                Price = 15,
                PromotionCode = "ABC",
                ExtendedGurantee = false
            };
            var orderItem4 = new OrderItem
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = "c0783cd6-1c9e-4c1c-8131-2c9583e2b191",
                OrderId = order2.Id,
                Quantity = 4,
                Price = 20,
                PromotionCode = "DEF",
                ExtendedGurantee = false
            };

            modelBuilder.Entity<OrderItem>().HasData(new List<OrderItem>
            { 
                orderItem1,
                orderItem2,
                orderItem3,
                orderItem4
            });
            #endregion
        }
    }
}