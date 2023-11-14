using FoodDeliveryAPI.Dtos;

namespace FoodDeliveryTests.Services
{
    [TestFixture]
    public class OrdersService_CreateOrderTests
    {
        [Test]
        public async Task CreateOrderAsync_ValidOrder_CreatesOrderSuccessfully()
        {
            // Arrange
            using var fakeDbContext = OrdersService_HelperMethods.GetFakeDbContext();
            var orderService = OrdersService_HelperMethods.GetOrderService(fakeDbContext);
            var newOrder = new CreateOrderDto
            {
                CustomerId = 1,
                RestaurantId = 1,
                SpecialInstructions = "Special intructions for the order",
                Products = new List<CreateOrderProductDto>
            {
                new CreateOrderProductDto { ProductId = 1, Amount=18,},
                new CreateOrderProductDto { ProductId = 2, Amount=36,},
            }
            };

            // Act
            await orderService.CreateOrderAsync(newOrder);

            // Assert
            Assert.IsTrue(fakeDbContext.Orders.Any(o => o.CustomerId.Equals(1) && o.RestaurantId.Equals(1)));

            var productIdsToCheck = new int[] { 1, 2 };
            var count = fakeDbContext.OrderProducts.Count(op => productIdsToCheck.Contains(op.ProductId));

            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public void CreateOrderAsync_NullOrder_ThrowsArgumentNullException()
        {
            // Arrange
            using var fakeDbContext = OrdersService_HelperMethods.GetFakeDbContext();
            var orderService = OrdersService_HelperMethods.GetOrderService(fakeDbContext);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await orderService.CreateOrderAsync(null));
        }

        [Test]
        public void CreateOrderAsync_NullProducts_ThrowsArgumentNullException()
        {
            // Arrange
            using var fakeDbContext = OrdersService_HelperMethods.GetFakeDbContext();
            var orderService = OrdersService_HelperMethods.GetOrderService(fakeDbContext);
            CreateOrderDto newOrder = new() { CustomerId = 1, RestaurantId = 1, Products = null };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await orderService.CreateOrderAsync(newOrder));
        }

        [Test]
        public void CreateOrderAsync_ProductsNotBelongingToRestaurant_ThrowsArgumentException()
        {
            // Arrange
            using var fakeDbContext = OrdersService_HelperMethods.GetFakeDbContext();
            var orderService = OrdersService_HelperMethods.GetOrderService(fakeDbContext);
            var newOrder = new CreateOrderDto
            {
                CustomerId = 1,
                RestaurantId = 1,
                SpecialInstructions = "Special intructions for the order",
                Products = new List<CreateOrderProductDto>
            {
                new CreateOrderProductDto { ProductId = 3, Amount=1,}
            }
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await orderService.CreateOrderAsync(newOrder));
            Assert.That(exception.Message, Is.EqualTo("Some products in the order, do not correspond to the restaurant used in the order"));
        }
    }

}
