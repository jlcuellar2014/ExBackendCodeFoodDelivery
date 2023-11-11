using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryAPI.Model
{
    public class OrderProduct
    {
        [Key]
        public int OrderId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public int Ammount { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
