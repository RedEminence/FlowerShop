namespace FlowerShop.Models
{
    public class Bouquet
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Flower> Flowers { get; set; } = new List<Flower>();

        public List<FlowerBouquet> FlowerBouquets { get; set; } = new List<FlowerBouquet>();

        public List<Order> Orders { get; set; }

        public string Image { get; set; }
    }
}
