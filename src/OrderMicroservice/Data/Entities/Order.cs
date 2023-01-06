namespace OrderMicroservice.Data.Entities;

public class Order : BaseEntity<string>
{
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}