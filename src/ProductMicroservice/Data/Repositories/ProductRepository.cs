using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Data.Entities;
using System.Data;

namespace ProductMicroservice.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAsync(IEnumerable<string> productIds)
        {
            return await _context.Products
                .Where(p => !p.IsDeleted && productIds.Contains(p.Id))
                .Include(p => p.ProductReviews)
                .ToListAsync();
        }

        public async Task<Product> AddAsync(Product product)
        {
            product.Id = Guid.NewGuid().ToString();

            var existingProduct = await GetAsync(new List<string> { product.Id });

            if (existingProduct is not null)
            {
                throw new DataException($"Add Product: Product with the given Id: {product.Id} already exists.");
            }

            var result = await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
