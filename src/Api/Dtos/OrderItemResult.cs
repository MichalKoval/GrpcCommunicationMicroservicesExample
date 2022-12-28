using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class OrderItemResult
{
    [Required]
    public ProductResult ProductInfo { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int Price { get; set; }

    public string PromotionCode { get; set; }
        
    [Required]
    public bool ExtendedGurantee { get; set; }
}