namespace FoodDeliveryAPI.Dtos
{
    public class CreateOrderProductDto
    {
        public required string ProductId { get; set; }
        public required int Amount { get; set; }
    }
}
