using ProductMicroservice.Data.Entities;

namespace ProductMicroservice.Data.Repositories;

public interface IProductReviewRepository
{
    Task<IEnumerable<ProductReview>> GetAsync(IEnumerable<string> productReviewIds);
    Task<ProductReview> AddAsync(ProductReview productReview);
}