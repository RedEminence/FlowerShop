using System.ComponentModel.DataAnnotations;

namespace FlowerShop.ViewModels
{
    public class OrderCreate
    {
        [Required]
        public int StatusId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime DeliverAt { get; set; }

        [Required]
        public int BouquetId { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }
    }
}
