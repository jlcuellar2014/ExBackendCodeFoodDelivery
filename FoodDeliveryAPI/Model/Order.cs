namespace FoodDeliveryAPI.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int? DeliveryVehicleId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string TrackingNumber { get; set; } = Guid.NewGuid().ToString();
        public string SpecialInstructions {  get; set; } = string.Empty;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public Customer? Customer { get; set; }
        public Restaurant? Restaurant { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
