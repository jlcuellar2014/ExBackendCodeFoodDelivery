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
        public required Customer Customer { get; set; }
        public required Restaurant Restaurant { get; set; }
        public required OrderProduct OrderProduct { get; set;}
    }
}
