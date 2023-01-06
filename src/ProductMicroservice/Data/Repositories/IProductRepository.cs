using ProductMicroservice.Data.Entities;

namespace ProductMicroservice.Data.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAsync(IEnumerable<string> productIds);
    Task<Product> AddAsync(Product product);
}