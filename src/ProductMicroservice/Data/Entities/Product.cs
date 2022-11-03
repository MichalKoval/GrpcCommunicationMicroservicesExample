namespace ProductMicroservice.Data.Entities
{
    public class Product : BaseEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductSize Size { get; set; }

        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
