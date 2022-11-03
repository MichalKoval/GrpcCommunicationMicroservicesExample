using System.ComponentModel.DataAnnotations.Schema;

namespace ProductMicroservice.Data.Entities
{
    public class ProductReview : BaseEntity<string>
    {
        public string Title { get; set; }
        public string Description { get; set; }    
        public int StartRating { get; set; }
        
        public string ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
