namespace FoodDeliveryAPI.Model
{
    public class DeliveryVehicle
    {
        public int DeliveryVehicleId { get; set; }
        public required string DeliverymanName { get; set; }
        public required string DeliverymanPhoneNumber { get; set; }
        public required string VehicleDescription { get; set; }
        public required string Coordinate { get; set; }
        public List<Order> Orders { get; set;} = new List<Order>();
        public List<DeliveryVehicleCoordinates> DeliveryVehicleCoordinates { get; set; } = new List<DeliveryVehicleCoordinates>();
    }
}
