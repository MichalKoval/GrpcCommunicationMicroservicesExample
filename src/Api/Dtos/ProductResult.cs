using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class ProductResult
    {
        [Required]
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public int Size { get; set; }
        public IList<ProductReviewResult> Reviews { get; set; }
        
    }
}
