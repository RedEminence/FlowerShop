namespace FlowerShop.Models
{
    public class Flower
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Bouquet> Bouquets { get; set; } = new List<Bouquet>();
        public List<FlowerBouquet> FlowerBouquets { get; set; } = new List<FlowerBouquet>();
    }
}
