using System.ComponentModel.DataAnnotations;

namespace FlowerShop.ViewModels
{
    public class OrderUpdate
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public DateTime DeliverAt { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }
    }
}
