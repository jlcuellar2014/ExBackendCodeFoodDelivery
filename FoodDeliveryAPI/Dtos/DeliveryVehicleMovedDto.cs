namespace FoodDeliveryAPI.Dtos
{
    public class DeliveryVehicleMovedDto
    {
        public int DeliveryVehicleId { get; set; }
        public required string Coordinate { get; set; }
        public DateTime DateTime { get; set; }
    }
}
