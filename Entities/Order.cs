namespace Apps.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerId {  get; set; }
        public User User { get; set; } = null!;

        public ICollection<OrderDetail> OrderDetials { get; set; } = null!;
    }
}
