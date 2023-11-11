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

        public required Product Product { get; set; }
        public required Order Order { get; set; }
    }
}
