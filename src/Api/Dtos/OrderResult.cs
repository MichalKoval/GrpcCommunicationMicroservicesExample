using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class OrderResult
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public List<OrderItemResult> OrderItems { get; set; }
    }
}
