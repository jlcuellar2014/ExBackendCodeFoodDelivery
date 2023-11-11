using FoodDeliveryAPI.Dtos;

namespace FoodDeliveryAPI.Services
{
    public class DeliveryVehiclesService : IDeliveryVehiclesService
    {
        public async Task UpdateDeliveryVehicleCoordinateAsync(string deliveryVehicleId, Coordinate coordinate)
        {
            await Task.Delay(100);
        }

        public async Task<Coordinate> GetDeliveryVehicleCoordinateAsync(string deliveryVehicleId)
        {
            await Task.Delay(100);
            return new Coordinate { Latitude = 45.25, Longitude = 84.35 };
        }

        public async Task CreateDeliveryVehicleOrderAsync(string deliveryVehicleId, string orderId)
        {
            await Task.Delay(100);
        }

        public async Task DeleteDeliveryVehicleOrderAsync(string deliveryVehicleId, string orderId)
        {
            await Task.Delay(100);
        }
    }
}
