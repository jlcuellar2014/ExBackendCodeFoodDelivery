using FoodDeliveryAPI.Model;
using FoodDeliveryAPI.Services;
using FoodDeliveryTests.Model;

namespace FoodDeliveryTests.Services
{
    public static class OrdersService_HelperMethods
    {
        public static FakeDbContext GetFakeDbContext()
        {
            var fakeDbContext = new FakeDbContext();

            fakeDbContext.Customers.Add(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Jorge",
                    PhoneNumber = "666351395",
                    ReceivingAddress = "Alicante"
                });

            fakeDbContext.Restaurants.AddRange(
                new Restaurant
                {
                    RestaurantId = 1,
                    RestaurantName = "Restaurant 1",
                    Coordinate = "1,1",
                    PhoneNumber = string.Empty,
                    PickUpAddress = string.Empty
                },
                new Restaurant
                {
                    RestaurantId = 2,
                    RestaurantName = "Restaurant 2",
                    Coordinate = "2,2",
                    PhoneNumber = string.Empty,
                    PickUpAddress = string.Empty
                });

            fakeDbContext.Products.AddRange(
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
                },
                new Product
                {
                    ProductId = 3,
                    RestaurantId = 2,
                    ProductName = "Product 3 Restauran 2",
                }
            );

            fakeDbContext.SaveChanges();

            return fakeDbContext;
        }

        public static IOrdersService GetOrderService(IFoodDeliveryContext dbContext)
        {
            return new OrdersService(dbContext);
        }
    }
}
