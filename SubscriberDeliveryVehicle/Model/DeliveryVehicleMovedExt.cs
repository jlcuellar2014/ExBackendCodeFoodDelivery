namespace SubscriberDeliveryVehicle.Model
{
    internal static class DeliveryVehicleMovedExt
    {
        public static string PlainFormat(this DeliveryVehicleMoved v)
            => $"The vehicle {v.DeliveryVehicleId} was moved at {v.DateTime.ToString()} to the coordinates: {v.Coordinate}";
    }
}
