using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Model;
using FoodDeliveryAPI.Services;
using FoodDeliveryTests.Model;

namespace FoodDeliveryTests.Services
{
    [TestFixture]
    public class OrdersService_CreateOrderTests
    {
        [Test]
        public async Task CreateOrderAsync_ValidOrder_CreatesOrderSuccessfully()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var orderService = new OrdersService(dbContext);

            dbContext.Restaurants.Add(
                new Restaurant
                {
                    RestaurantId = 1,
                    RestaurantName = "Restaurant",
                    Coordinate = "1,1",
                    PhoneNumber = string.Empty,
                    PickUpAddress = string.Empty
                });

            dbContext.Products.AddRange(
                new Product
                {
                    ProductId = 1,
                    RestaurantId = 1,
                    ProductName = "Product 1 Restauran 1",
                },
                new Product
                {
                    ProductId = 2,
                    RestaurantId = 1,
                    ProductName = "Product 2 Restauran 1",
                });

            await dbContext.SaveChangesAsync();

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
            Assert.IsTrue(dbContext.Orders.Any(o => o.CustomerId.Equals(1) && o.RestaurantId.Equals(1)));

            var productIdsToCheck = new int[] { 1, 2 };
            var count = dbContext.OrderProducts.Count(op => productIdsToCheck.Contains(op.ProductId));

            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public void CreateOrderAsync_NullOrder_ThrowsArgumentNullException()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var orderService = new OrdersService(dbContext);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await orderService.CreateOrderAsync(null));
        }

        [Test]
        public void CreateOrderAsync_NullProducts_ThrowsArgumentNullException()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var orderService = new OrdersService(dbContext);

            CreateOrderDto newOrder = new() { CustomerId = 1, RestaurantId = 1, Products = null };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await orderService.CreateOrderAsync(newOrder));
        }

        [Test]
        public async Task CreateOrderAsync_ProductsNotBelongingToRestaurant_ThrowsArgumentExceptionAsync()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var orderService = new OrdersService(dbContext);

            dbContext.Restaurants.Add(
               new Restaurant
               {
                   RestaurantId = 1,
                   RestaurantName = "Restaurant",
                   Coordinate = "1,1",
                   PhoneNumber = string.Empty,
                   PickUpAddress = string.Empty
               });

            dbContext.Products.Add(
                new Product
                {
                    ProductId = 1,
                    RestaurantId = 2,
                    ProductName = "Product 1 Restauran 2"
                });

            await dbContext.SaveChangesAsync();

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
