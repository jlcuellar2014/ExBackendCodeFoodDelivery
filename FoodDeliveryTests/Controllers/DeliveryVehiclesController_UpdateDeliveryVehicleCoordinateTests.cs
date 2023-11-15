using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodDeliveryAPI.Controllers.Tests
{
    [TestFixture]
    public class DeliveryVehiclesController_UpdateDeliveryVehicleCoordinateTests
    {
        [Test]
        public async Task ValidCoordinate_ReturnsOkResult()
        {
            // Arrange
            var vehiclesServiceMock = new Mock<IDeliveryVehiclesService>();
            var controller = new DeliveryVehiclesController(vehiclesServiceMock.Object);
            var deliveryVehicleId = 1;
            var validCoordinate = new CoordinateDto { Latitude = 123.456, Longitude = 789.012 };

            // Act
            var result = await controller.UpdateDeliveryVehicleCoordinateAsync(deliveryVehicleId, validCoordinate);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            vehiclesServiceMock.Verify(x => x.UpdateDeliveryVehicleCoordinateAsync(deliveryVehicleId, validCoordinate), Times.Once);
        }

        [Test]
        public async Task InvalidCoordinate_ReturnsBadRequest()
        {
            // Arrange
            var vehiclesServiceMock = new Mock<IDeliveryVehiclesService>();
            var controller = new DeliveryVehiclesController(vehiclesServiceMock.Object);
            var deliveryVehicleId = 1;
            CoordinateDto invalidCoordinate = null;

            // Act
            var result = await controller.UpdateDeliveryVehicleCoordinateAsync(deliveryVehicleId, invalidCoordinate);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            vehiclesServiceMock.Verify(x => x.UpdateDeliveryVehicleCoordinateAsync(deliveryVehicleId, It.IsAny<CoordinateDto>()), Times.Never);
        }
    }
}