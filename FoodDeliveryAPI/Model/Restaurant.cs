namespace FoodDeliveryAPI.Model
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public required string RestaurantName { get; set; }
        public required string PickUpAddress { get; set; }
        public required string PhoneNumber { get; set;}
        public required string Coordinate { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Product> ProductRestaurants { get; set; } = new List<Product>();
    }
}
