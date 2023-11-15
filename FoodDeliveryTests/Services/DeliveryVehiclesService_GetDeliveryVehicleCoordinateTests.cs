using FoodDeliveryAPI.Model;
using FoodDeliveryAPI.Services;
using FoodDeliveryTests.Model;
using FoodDeliveryTests.Publishers;
using FoodDeliveryTests.Utilities;

namespace FoodDeliveryTests.Services
{

    [TestFixture]
    public class DeliveryVehiclesService_GetDeliveryVehicleCoordinateTests
    {
        [Test]
        public async Task GetDeliveryVehicleCoordinateAsync_ValidId_ReturnsCoordinateDto()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var deliveryService = new DeliveryVehiclesService(dbContext, new FakePublisher());

            var vehicle = new DeliveryVehicle { 
                DeliveryVehicleId = 1,
                Coordinate = "1,1",
                DeliverymanName=string.Empty,
                DeliverymanPhoneNumber=string.Empty,
                VehicleDescription=string.Empty
            };

            dbContext.DeliveryVehicles.Add(vehicle);
            await dbContext.SaveChangesAsync();

            // Act
            var coordinates = await deliveryService.GetDeliveryVehicleCoordinateAsync(1);

            // Assert
            Assert.IsNotNull(coordinates);
            Assert.That(coordinates.ToPlainFormat(), Is.EqualTo("1,1"));
        }

        [Test]
        public void GetDeliveryVehicleCoordinateAsync_InvalidId_ThrowsException()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var deliveryService = new DeliveryVehiclesService(dbContext, new FakePublisher());

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => deliveryService.GetDeliveryVehicleCoordinateAsync(1));
        }
    }
}
