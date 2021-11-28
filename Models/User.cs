using Microsoft.AspNetCore.Identity;


namespace FlowerShop.Models
{
    public class User : IdentityUser    
    {
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
