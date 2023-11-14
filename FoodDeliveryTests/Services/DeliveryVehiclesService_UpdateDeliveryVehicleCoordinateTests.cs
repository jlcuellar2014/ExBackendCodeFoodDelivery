using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Model;
using FoodDeliveryAPI.Services;
using FoodDeliveryTests.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTests.Services
{
    [TestFixture]
    public class DeliveryVehiclesService_UpdateDeliveryVehicleCoordinateTests
    {
        [Test]
        public async Task UpdateDeliveryVehicleCoordinateAsync_ValidId_UpdatesCoordinateAndAddsHistorical()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var deliveryService = new DeliveryVehiclesService(dbContext);

            var vehicle = new DeliveryVehicle { 
                DeliveryVehicleId = 1, 
                Coordinate = "0,0",
                DeliverymanName = string.Empty,
                DeliverymanPhoneNumber = string.Empty,
                VehicleDescription = string.Empty,
            };

            dbContext.DeliveryVehicles.Add(vehicle);
            await dbContext.SaveChangesAsync();

            var newCoordinate = new CoordinateDto { Latitude = 1, Longitude = 1 };

            // Act
            await deliveryService.UpdateDeliveryVehicleCoordinateAsync(1, newCoordinate);

            // Assert
            var updatedVehicle = await dbContext.DeliveryVehicles.FindAsync(1);
            var historicalCoordinate = await dbContext.DeliveryVehicleCoordinates.FirstOrDefaultAsync();

            Assert.That(updatedVehicle, Is.Not.Null);
            Assert.That(updatedVehicle.Coordinate, Is.EqualTo("1,1"));

            Assert.IsNotNull(historicalCoordinate);
            Assert.That(historicalCoordinate.Coordinate, Is.EqualTo("1,1"));
        }

        [Test]
        public void UpdateDeliveryVehicleCoordinateAsync_InvalidId_ThrowsException()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var deliveryService = new DeliveryVehiclesService(dbContext);

            var newCoordinate = new CoordinateDto { Latitude = 1, Longitude = 1 };

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => deliveryService.UpdateDeliveryVehicleCoordinateAsync(1, newCoordinate));
        }
    }
}
