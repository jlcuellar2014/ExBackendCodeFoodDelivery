namespace FoodDeliveryAPI.Dtos
{
    public class CreateOrderDto
    {
        public required int CustomerId { get; set; }
        public required int RestaurantId { get; set; }
        public string? SpecialInstructions { get; set; }
        public required List<CreateOrderProductDto> Products { get; set;}
    }
}
