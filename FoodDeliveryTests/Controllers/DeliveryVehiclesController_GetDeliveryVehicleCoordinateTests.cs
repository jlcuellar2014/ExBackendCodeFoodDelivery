using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodDeliveryAPI.Controllers.Tests
{
    [TestFixture]
    public class DeliveryVehiclesController_GetDeliveryVehicleCoordinateTests
    {
        [Test]
        public async Task ValidId_ReturnsOkResultWithCoordinate()
        {
            // Arrange
            var vehiclesServiceMock = new Mock<IDeliveryVehiclesService>();
            var controller = new DeliveryVehiclesController(vehiclesServiceMock.Object);
            var deliveryVehicleId = 1;
            var expectedCoordinate = new CoordinateDto { Latitude = 123.456, Longitude = 789.012 };
            vehiclesServiceMock.Setup(x => x.GetDeliveryVehicleCoordinateAsync(deliveryVehicleId)).ReturnsAsync(expectedCoordinate);

            // Act
            var result = await controller.GetDeliveryVehicleCoordinateAsync(deliveryVehicleId);

            // Assert
            Assert.IsInstanceOf<ActionResult<CoordinateDto>>(result);

            var actionResult = result as ActionResult<CoordinateDto>;
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);

            var coordinate = (actionResult.Result as OkObjectResult)?.Value as CoordinateDto;
            Assert.IsNotNull(coordinate);
            Assert.That(coordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public async Task InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var vehiclesServiceMock = new Mock<IDeliveryVehiclesService>();
            var controller = new DeliveryVehiclesController(vehiclesServiceMock.Object);
            var invalidDeliveryVehicleId = -1;

            // Act
            var result = await controller.GetDeliveryVehicleCoordinateAsync(invalidDeliveryVehicleId);

            // Assert
            Assert.IsInstanceOf<ActionResult<CoordinateDto>>(result);

            var actionResult = result as ActionResult<CoordinateDto>;
            Assert.IsNotNull(actionResult);

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult.Result);
        }
    }
}