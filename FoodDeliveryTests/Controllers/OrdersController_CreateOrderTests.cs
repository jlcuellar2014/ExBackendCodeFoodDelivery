using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodDeliveryAPI.Controllers.Tests
{
    [TestFixture()]
    public class OrdersController_CreateOrderTests
    {
        [Test]
        public async Task CreateOrderAsync_ValidOrder_ReturnsOkResult()
        {
            // Arrange
            var ordersServiceMock = new Mock<IOrdersService>();
            var controller = new OrdersController(ordersServiceMock.Object);
            var validOrder = new CreateOrderDto
            {
                CustomerId = 1,
                RestaurantId = 1,
                Products = new List<CreateOrderProductDto> {
                    new CreateOrderProductDto { ProductId = 1, Amount = 1 }
                }
            };

            // Act
            var result = await controller.CreateOrderAsync(validOrder);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            ordersServiceMock.Verify(x => x.CreateOrderAsync(validOrder), Times.Once);
        }

        [Test]
        public async Task CreateOrderAsync_NullOrder_ReturnsBadRequest()
        {
            // Arrange
            var ordersServiceMock = new Mock<IOrdersService>();
            var controller = new OrdersController(ordersServiceMock.Object);

            // Act
            var result = await controller.CreateOrderAsync(null);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            ordersServiceMock.Verify(x => x.CreateOrderAsync(It.IsAny<CreateOrderDto>()), Times.Never);
        }

        [Test]
        public async Task CreateOrderAsync_ServiceThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var ordersServiceMock = new Mock<IOrdersService>();
            ordersServiceMock.Setup(x => x.CreateOrderAsync(It.IsAny<CreateOrderDto>())).ThrowsAsync(new Exception("Simulated exception"));
            var controller = new OrdersController(ordersServiceMock.Object);
            var validOrder = new CreateOrderDto
            {
                CustomerId = 1,
                RestaurantId = 1,
                Products = new List<CreateOrderProductDto> {
                    new CreateOrderProductDto { ProductId = 1, Amount = 1 }
                }
            };

            // Act
            var result = await controller.CreateOrderAsync(validOrder);

            // Assert
            var actionResult = result as ActionResult;
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<BadRequestResult>(actionResult);
        }
    }
}