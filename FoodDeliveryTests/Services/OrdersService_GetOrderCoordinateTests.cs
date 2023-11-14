using FoodDeliveryAPI.Model;
using FoodDeliveryAPI.Services;
using FoodDeliveryTests.Model;
using FoodDeliveryTests.Utilities;

namespace FoodDeliveryTests.Services
{
    [TestFixture]
    public class OrdersService_GetOrderCoordinateTests
    {
        [Test]
        public async Task GetOrderCoordinateAsync_OrderExists_ReturnsCoordinateDto()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var orderService = new OrdersService(dbContext);

            dbContext.Orders.Add(
                new Order
                {
                    TrackingNumber = "123",
                    OrderStatus = OrderStatus.Pending,
                    RestaurantId = 1
                });

            dbContext.Restaurants.Add(
                new Restaurant
                {
                    RestaurantId = 1,
                    RestaurantName = "Restaurant",
                    Coordinate = "1,1",
                    PhoneNumber = string.Empty,
                    PickUpAddress = string.Empty
                });

            await dbContext.SaveChangesAsync();

            // Act
            var coordinate = await orderService.GetOrderCoordinateAsync("123");

            // Assert
            Assert.IsNotNull(coordinate);
            Assert.That(coordinate.ToPlainFormat(), Is.EqualTo("1,1"));
        }

        [Test]
        public async Task GetOrderCoordinateAsync_OrderOnTheWayWithVehicle_ReturnsCoordinateDto()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var orderService = new OrdersService(dbContext);

            dbContext.Orders.Add(
                new Order
                {
                    TrackingNumber = "123",
                    OrderStatus = OrderStatus.OnTheWay,
                    RestaurantId = 1,
                    DeliveryVehicleId = 1
                });

            dbContext.DeliveryVehicles.Add(
                new DeliveryVehicle
                {
                    DeliveryVehicleId = 1,
                    Coordinate = "1,1",
                    DeliverymanName = "Delivery",
                    DeliverymanPhoneNumber = string.Empty,
                    VehicleDescription = string.Empty,
                });

            await dbContext.SaveChangesAsync();

            // Act
            var coordinate = await orderService.GetOrderCoordinateAsync("123");

            // Assert
            Assert.IsNotNull(coordinate);
            Assert.That(coordinate.ToPlainFormat(), Is.EqualTo("1,1"));
        }

        [Test]
        public async Task GetOrderCoordinateAsync_OrderDelivered_ThrowsException()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var orderService = new OrdersService(dbContext);

            dbContext.Orders.Add(
                new Order { 
                    TrackingNumber = "456", 
                    OrderStatus = OrderStatus.Delivered
                });

            await dbContext.SaveChangesAsync();

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => orderService.GetOrderCoordinateAsync("456"));
        }

        [Test]
        public async Task GetOrderCoordinateAsync_OrderOnTheWayWithoutVehicle_ThrowsException()
        {
            // Arrange
            using var dbContext = new FakeDbContext();
            var orderService = new OrdersService(dbContext);

            dbContext.Orders.Add(
                new Order { 
                    TrackingNumber = "101", 
                    OrderStatus = OrderStatus.OnTheWay, 
                    DeliveryVehicleId = null 
                });

            await dbContext.SaveChangesAsync();

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => orderService.GetOrderCoordinateAsync("101"));
        }
    }
}
