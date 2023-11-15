using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Model;
using FoodDeliveryAPI.Publishers;

namespace FoodDeliveryAPI.Services
{
    public class DeliveryVehiclesService : IDeliveryVehiclesService
    {
        private readonly IFoodDeliveryContext dbContext;
        private readonly IMsgPublisher msgPublisher;

        public DeliveryVehiclesService(IFoodDeliveryContext dbContext, IMsgPublisher msgPublisher)
        {
            this.dbContext = dbContext;
            this.msgPublisher = msgPublisher;
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
            var newDeliveryVehicleCoordinate = new DeliveryVehicleCoordinates
            {
                Coordinate = vehicle.Coordinate,
                DeliveryVehicle = vehicle
            };

            await dbContext.DeliveryVehicleCoordinates.AddAsync(newDeliveryVehicleCoordinate);

            await dbContext.SaveChangesAsync();

            msgPublisher.PublishDeliveryVehicheMovedEvent(
                new DeliveryVehicleMovedDto
                {
                    Coordinate = newDeliveryVehicleCoordinate.Coordinate,
                    DeliveryVehicleId = newDeliveryVehicleCoordinate.DeliveryVehicleId,
                    DateTime = newDeliveryVehicleCoordinate.DateTime
                });
        }

        public async Task<CoordinateDto> GetDeliveryVehicleCoordinateAsync(int deliveryVehicleId)
        {
            var vehicle = await dbContext.DeliveryVehicles.FindAsync(deliveryVehicleId);

            if (vehicle == null)
            {
                throw new ArgumentException($"No Delivery Vehicle found with the ID  {deliveryVehicleId}");
            }

            return new CoordinateDto(vehicle.Coordinate);
        }

        private readonly object createOrderLock = new object();
        public void CreateDeliveryVehicleOrder(int deliveryVehicleId, int orderId)
        {
            lock (createOrderLock)
            {
                var order = dbContext.Orders.Find(orderId);

                if (order == null)
                {
                    throw new ArgumentException($"No Order found with the ID  {orderId}");
                }

                if (order.DeliveryVehicleId != null)
                    throw new InvalidOperationException($"Order with ID {orderId} " +
                        $"has already been assigned to another Delivery Vehicle with ID {order.DeliveryVehicleId}");

                order.DeliveryVehicleId = deliveryVehicleId;
                dbContext.SaveChanges();
            }
        }

        public async Task DeleteDeliveryVehicleOrderAsync(int deliveryVehicleId, int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);

            if (order == null)
            {
                throw new ArgumentException($"No Order found with the ID  {orderId}");
            }

            order.DeliveryVehicleId = null;
            await dbContext.SaveChangesAsync();
        }
    }
}
