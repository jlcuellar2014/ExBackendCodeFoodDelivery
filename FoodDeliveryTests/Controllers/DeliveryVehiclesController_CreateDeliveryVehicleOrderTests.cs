using FoodDeliveryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodDeliveryAPI.Controllers.Tests
{
    [TestFixture]
    public class DeliveryVehiclesController_CreateDeliveryVehicleOrderTests
    {
        [Test]
        public async Task ValidIds_ReturnsOkResult()
        {
            // Arrange
            var vehiclesServiceMock = new Mock<IDeliveryVehiclesService>();
            var controller = new DeliveryVehiclesController(vehiclesServiceMock.Object);
            var deliveryVehicleId = 1;
            var orderId = 42;

            // Act
            var result = await controller.CreateDeliveryVehicleOrderAsync(deliveryVehicleId, orderId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            vehiclesServiceMock.Verify(x => x.CreateDeliveryVehicleOrderAsync(deliveryVehicleId, orderId), Times.Once);
        }
    }
}