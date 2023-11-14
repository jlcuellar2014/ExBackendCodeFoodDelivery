using FoodDeliveryAPI.Model;
using FoodDeliveryAPI.Dtos;
using FoodDeliveryTests.Model;

namespace FoodDeliveryAPI.Services.Tests
{
    [TestFixture()]
    public class OrdersServiceTests
    {
        [Test]
        public void CreateOrder_NullNewOrder_ThrowArgumentNullException()
        {
            // Arrange
            var fakeDbContext = new FakeDbContext();
            var ordersService = new OrdersService(fakeDbContext);
            CreateOrderDto newOrder = null;

            // Act y Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => ordersService.CreateOrderAsync(newOrder));
        }

        [Test]
        public void CreateOrder_NullListProductsInOrden_ThrowArgumentNullException()
        {
            // Arrange
            var fakeDbContext = new FakeDbContext();
            var ordersService = new OrdersService(fakeDbContext);
            CreateOrderDto newOrder = new() { CustomerId = 1, RestaurantId = 1, Products = null };

            // Act y Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => ordersService.CreateOrderAsync(newOrder));
        }

        [Test]
        public void CreateOrder_ProductsWithWrongRestaurantId_ThrowArgumentException()
        {
            // Arrange
            var fakeDbContext = new FakeDbContext();

            fakeDbContext.Restaurants.Add(new Restaurant
            {
                RestaurantId = 1,
                RestaurantName = "Restaurant",
                Coordinate = string.Empty,
                PhoneNumber = string.Empty,
                PickUpAddress = string.Empty
            });

            fakeDbContext.Products.Add(new Product
            {
                ProductId = 1,
                RestaurantId = 2, // This product not correspond to the restaurant
                ProductName = "Product",
            });

            fakeDbContext.SaveChanges();

            var ordersService = new OrdersService(fakeDbContext);

            var newOrder = new CreateOrderDto
            {
                CustomerId = 1,
                RestaurantId = 1,
                Products = new List<CreateOrderProductDto> {
                    new CreateOrderProductDto{ProductId = 1}
                }
            };

            // Act y Assert
            Assert.ThrowsAsync<ArgumentException>(() => ordersService.CreateOrderAsync(newOrder));
        }
    }
}