using FoodDeliveryAPI.Dtos;

namespace FoodDeliveryAPI.Services
{
    public class DeliveryVehiclesService : IDeliveryVehiclesService
    {
        public async Task UpdateDeliveryVehicleCoordinateAsync(string deliveryVehicleId, CoordinateDto coordinate)
        {
            await Task.Delay(100);
        }

        public async Task<CoordinateDto> GetDeliveryVehicleCoordinateAsync(string deliveryVehicleId)
        {
            await Task.Delay(100);
            return new CoordinateDto { Latitude = 45.25, Longitude = 84.35 };
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
