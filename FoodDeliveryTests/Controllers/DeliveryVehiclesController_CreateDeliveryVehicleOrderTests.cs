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
            var result = controller.CreateDeliveryVehicleOrder(deliveryVehicleId, orderId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            vehiclesServiceMock.Verify(x => x.CreateDeliveryVehicleOrder(deliveryVehicleId, orderId), Times.Once);
        }
    }
}