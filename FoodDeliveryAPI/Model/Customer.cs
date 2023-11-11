namespace FoodDeliveryAPI.Model
{
    public class Customer
    {
        public required int CustomerId { get; set; }
        public required string CustomerName { get; set; }
        public required string ReceivingAddress { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
