using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryAPI.Model
{
    public class OrderProduct
    {
        [Key]
        public int OrderId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public int Ammount { get; set; } = 1;

        public Product? Product { get; set; }
        public Order? Order { get; set; }
    }
}
