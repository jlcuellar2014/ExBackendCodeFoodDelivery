using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Model;

namespace FoodDeliveryAPI.Services
{
    public class DeliveryVehiclesService : IDeliveryVehiclesService
    {
        private readonly FoodDeliveryContext dbContext;

        public DeliveryVehiclesService()
        {
            dbContext = new FoodDeliveryContext();

        }

        public async Task UpdateDeliveryVehicleCoordinateAsync(int deliveryVehicleId, CoordinateDto coordinate)
        {
            string auxCoordinate = $"{coordinate.Latitude},{coordinate.Longitude}";

            var vehicle = await dbContext.DeliveryVehicles.FindAsync(deliveryVehicleId);

            if (vehicle == null) {
                throw new Exception($"No Delivery Vehicle found with the ID  {deliveryVehicleId}");
            }

            vehicle.Coordinate = auxCoordinate;

            // Save the historical of the coordinates
            await dbContext.DeliveryVehicleCoordinates.AddAsync(new DeliveryVehicleCoordinates
            {
                Coordinate = vehicle.Coordinate,
                DeliveryVehicle = vehicle
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task<CoordinateDto> GetDeliveryVehicleCoordinateAsync(int deliveryVehicleId)
        {
            await Task.Delay(100);
            return new CoordinateDto { Latitude = 45.25, Longitude = 84.35 };
        }

        public async Task CreateDeliveryVehicleOrderAsync(int deliveryVehicleId, int orderId)
        {
            await Task.Delay(100);
        }

        public async Task DeleteDeliveryVehicleOrderAsync(int deliveryVehicleId, int orderId)
        {
            await Task.Delay(100);
        }
    }
}
