namespace FlowerShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int StatusId { get; set; }
        public OrderStatus Status { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime DeliverAt { get; set; }

        public int BouquetId { get; set; }
        public Bouquet Bouquet { get; set; }

        public string Address { get; set; }
    }
}
