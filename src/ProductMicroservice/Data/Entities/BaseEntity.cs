namespace ProductMicroservice.Data.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
