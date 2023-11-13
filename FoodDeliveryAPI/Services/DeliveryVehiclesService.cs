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

            if (vehicle == null)
            {
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
            var vehicle = await dbContext.DeliveryVehicles.FindAsync(deliveryVehicleId);

            if (vehicle == null)
            {
                throw new Exception($"No Delivery Vehicle found with the ID  {deliveryVehicleId}");
            }

            return new CoordinateDto(vehicle.Coordinate);
        }

        public async Task CreateDeliveryVehicleOrderAsync(int deliveryVehicleId, int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            
            if(order == null)
            {
                throw new Exception($"No Order found with the ID  {orderId}");
            }

            order.DeliveryVehicleId = deliveryVehicleId;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteDeliveryVehicleOrderAsync(int deliveryVehicleId, int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);

            if (order == null)
            {
                throw new Exception($"No Order found with the ID  {orderId}");
            }

            order.DeliveryVehicleId = null;
            await dbContext.SaveChangesAsync();
        }
    }
}
