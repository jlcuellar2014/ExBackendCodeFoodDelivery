namespace FoodDeliveryAPI.Model
{
    public class DeliveryVehicleCoordinates
    {
        public int DeliveryVehicleCoordinatesId { get; set; }
        public int DeliveryVehicleId { get; set; }
        public required string Coordinate {  get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public DeliveryVehicle DeliveryVehicle { get; set; }
    }
}
