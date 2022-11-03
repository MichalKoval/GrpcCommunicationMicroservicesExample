using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Data.Entities;

namespace ProductMicroservice.Data
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ProductDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ProductSeed
            var product1 = new Product
            {
                Id = "1bebc704-c7f6-4f77-beed-52ed08ed0716",
                Name = "Cappuccino",
                Description = "A cappuccino is an espresso-based coffee drink that originated in Italy and is prepared with steamed milk foam (microfoam).",
                Size = ProductSize.LARGE,                
            };
            var product2 = new Product
            {
                Id = "c0783cd6-1c9e-4c1c-8131-2c9583e2b191",
                Name = "Brew",
                Description = "Brewed coffee is made by pouring hot water onto ground coffee beans, then allowing to brew.",
                Size = ProductSize.MEDIUM
            };
            var product3 = new Product
            {
                Id = "c126f115-4905-448e-a00f-c33a3c79a8e5",
                Name = "Espresso",
                Description = "Espresso is the name of a highly concentrated, bittersweet coffee.",
                Size = ProductSize.SMALL
            };

            modelBuilder.Entity<Product>().HasData(new List<Product> { product1, product2, product3 });
            #endregion

            #region ProductReviewSeed
            var productReview1 = new ProductReview
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product1.Id,
                Title = "Tastes Great!",
                Description = "Considering its convenience to be able to brew coffee during your busy working hours, this actually tastes great.",
                StartRating = 5              
            };
            var productReview2 = new ProductReview
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product2.Id,
                Title = "Not that sweet",
                Description = "Doesn't taste sweet no matter how it is prepared.",
                StartRating = 4
            };
            var productReview3 = new ProductReview
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product3.Id,
                Title = "Very strong!",
                Description = "This espresso is the strongest.",
                StartRating = 5
            };

            modelBuilder.Entity<ProductReview>().HasData(new List<ProductReview> { productReview1, productReview2, productReview3 });
            #endregion
        }
    }
}