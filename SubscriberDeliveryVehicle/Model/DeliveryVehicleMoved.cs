namespace SubscriberDeliveryVehicle.Model
{
    public class DeliveryVehicleMoved
    {
        public int DeliveryVehicleId { get; set; }
        public required string Coordinate { get; set; }
        public DateTime DateTime { get; set; }
    }
}