using System.ComponentModel.DataAnnotations;

namespace ProductMicroservice.Data.Entities
{
    public class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
