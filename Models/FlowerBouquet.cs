namespace FlowerShop.Models
{
    public class FlowerBouquet
    {

        public int FlowerId {get; set; }
        public Flower Flower { get; set; }

        public int BouquetId { get; set; }
        public Bouquet Bouquet { get; set; }

        public int FlowerCount { get; set; }
    }
}
