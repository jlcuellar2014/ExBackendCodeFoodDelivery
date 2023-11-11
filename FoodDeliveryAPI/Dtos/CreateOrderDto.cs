namespace FoodDeliveryAPI.Dtos
{
    public class CreateOrderDto
    {
        public required string CustomerId { get; set; }
        public required string RestaurantId { get; set; }
        public string? SpecialInstructions { get; set; }
        public required List<CreateOrderProductDto> Products { get; set;}
    }
}
