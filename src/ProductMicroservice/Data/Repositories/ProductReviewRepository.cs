using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Data.Entities;
using System.Data;

namespace ProductMicroservice.Data.Repositories;

public class ProductReviewRepository : IProductReviewRepository
{
    private readonly ProductContext _context;

    public ProductReviewRepository(ProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductReview>> GetAsync(IEnumerable<string> productReviewIds)
    {
        return await _context.ProductReviews
            .Where(o => !o.IsDeleted && productReviewIds.Contains(o.Id))
            .ToListAsync();
    }

    public async Task<ProductReview> AddAsync(ProductReview productReview)
    {
        productReview.Id = Guid.NewGuid().ToString();

        var existingProduct = await GetAsync(new List<string> { productReview.Id });

        if (existingProduct is not null)
        {
            throw new DataException($"Add Product Review: Product review with the given Id: {productReview.Id} already exists.");
        }

        var result = await _context.ProductReviews.AddAsync(productReview);

        await _context.SaveChangesAsync();

        return result.Entity;
    }
}