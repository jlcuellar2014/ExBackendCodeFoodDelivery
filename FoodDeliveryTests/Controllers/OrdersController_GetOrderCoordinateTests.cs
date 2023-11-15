using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodDeliveryAPI.Controllers.Tests
{
    [TestFixture()]
    public class OrdersController_GetOrderCoordinateTests
    {
        [Test]
        public async Task GetOrderCoordinateAsync_ValidTrackingNumber_ReturnsOkResultWithCoordinate()
        {
            // Arrange
            var ordersServiceMock = new Mock<IOrdersService>();
            var controller = new OrdersController(ordersServiceMock.Object);
            var validTrackingNumber = "123456";

            // Configurar el servicio para devolver una coordenada simulada
            var expectedCoordinate = new CoordinateDto { Latitude = 123.456, Longitude = 789.012 };
            ordersServiceMock.Setup(x => x.GetOrderCoordinateAsync(validTrackingNumber)).ReturnsAsync(expectedCoordinate);

            // Act
            var result = await controller.GetOrderCoordinateAsync(validTrackingNumber);

            // Assert
            var actionResult = result as ActionResult<CoordinateDto>;
            var coordinate = (actionResult.Result as OkObjectResult)?.Value as CoordinateDto;

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.IsNotNull(coordinate);

            Assert.That(coordinate.Latitude, Is.EqualTo(expectedCoordinate.Latitude));
            Assert.That(coordinate.Longitude, Is.EqualTo(expectedCoordinate.Longitude));
        }

        [Test]
        public void GetOrderCoordinateAsync_NullTrackingNumber_ThrowsArgumentException()
        {
            // Arrange
            var ordersServiceMock = new Mock<IOrdersService>();
            var controller = new OrdersController(ordersServiceMock.Object);

            // Act y Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await controller.GetOrderCoordinateAsync(null));
            ordersServiceMock.Verify(x => x.GetOrderCoordinateAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void GetOrderCoordinateAsync_EmptyTrackingNumber_ThrowsArgumentException()
        {
            // Arrange
            var ordersServiceMock = new Mock<IOrdersService>();
            var controller = new OrdersController(ordersServiceMock.Object);

            // Act y Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await controller.GetOrderCoordinateAsync(string.Empty));
            ordersServiceMock.Verify(x => x.GetOrderCoordinateAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetOrderCoordinateAsync_ServiceThrowsException_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var ordersServiceMock = new Mock<IOrdersService>();
            ordersServiceMock.Setup(x => x.GetOrderCoordinateAsync(It.IsAny<string>())).ThrowsAsync(new Exception("Simulated exception"));
            var controller = new OrdersController(ordersServiceMock.Object);
            var validTrackingNumber = "123456";

            // Act
            var result = await controller.GetOrderCoordinateAsync(validTrackingNumber);

            // Assert
            var actionResult = result as ActionResult<CoordinateDto>;

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult.Result);
            var expectedMessage = $"No Coordinate found for Traking Number {validTrackingNumber}";
            Assert.That((actionResult.Result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
        }
    }
}