namespace FoodDeliveryAPI.Model
{
    public class DeliveryVehicleCoordinates
    {
        public int DeliveryVehicleCoordinatesId { get; set; }
        public int DeliveryVehicleId { get; set; }
        public required string Coordinate {  get; set; }
        public required DateTime DateTime { get; set; }
        public required DeliveryVehicle DeliveryVehicle { get; set; }
    }
}
