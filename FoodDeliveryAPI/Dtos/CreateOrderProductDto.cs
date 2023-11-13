namespace FoodDeliveryAPI.Dtos
{
    public class CreateOrderProductDto
    {
        public required int ProductId { get; set; }
        public int Amount { get; set; } = 1;
    }
}
