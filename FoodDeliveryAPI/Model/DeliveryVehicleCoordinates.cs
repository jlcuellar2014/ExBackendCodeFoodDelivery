namespace FoodDeliveryAPI.Model
{
    public class DeliveryVehicleCoordinates
    {
        public int DeliveryVehicleCoordinatesId { get; set; }
        public int DeliveryVehicleId { get; set; }
        public required Coordinate Coordinate {  get; set; } 
        public DateTime DateTime { get; set; }
        public required DeliveryVehicle DeliveryVehicle { get; set; }
    }
}
