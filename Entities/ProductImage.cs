namespace Apps.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = string.Empty!;
        public Boolean IsMain { get; set; } = false;

        public DateTime CreatedAt { get; set; }
    }
}
