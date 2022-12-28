using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class ProductReviewResult
{
    [Required]
    public string Title { get; set; }
        
    [Required]
    public string Description { get; set; }
        
    public int StartRating { get; set; }
}