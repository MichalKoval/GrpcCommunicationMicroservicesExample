using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class GetOrdersResult
{
    [Required]
    public IEnumerable<OrderResult> Orders { get; set; }
}