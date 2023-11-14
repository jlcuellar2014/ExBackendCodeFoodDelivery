namespace FoodDeliveryAPI.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public int RestaurantId { get; set; }
        public required string ProductName { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Restaurant? Restaurant { get; set; }
    }
}
