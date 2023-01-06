using System.ComponentModel.DataAnnotations.Schema;

namespace OrderMicroservice.Data.Entities;

public class OrderItem : BaseEntity<string>
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
    public string PromotionCode { get; set; }
    public bool ExtendedGurantee { get; set; }

    public string OrderId { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }
}