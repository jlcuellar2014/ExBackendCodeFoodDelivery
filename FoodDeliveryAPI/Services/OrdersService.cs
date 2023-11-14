using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryAPI.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IFoodDeliveryContext dbContext;
        public OrdersService(IFoodDeliveryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateOrderAsync(CreateOrderDto order)
        {
            var newOrderProducts = new List<OrderProduct>();
            var newOrder = new Order
            {
                CustomerId = order.CustomerId,
                RestaurantId = order.RestaurantId,
                SpecialInstructions = order.SpecialInstructions ?? string.Empty,
            };

            foreach (var p in order.Products)
            {
                newOrderProducts.Add(new OrderProduct
                {
                    OrderId = newOrder.OrderId,
                    ProductId = p.ProductId,
                    Ammount = p.Amount,
                    Order = newOrder
                });
            }

            // Verifying that the products correspond to the restaurant of the order
            var auxIdProducts = from p in newOrderProducts
                                select p.ProductId;

            var badRestaurantProductRelationship = await dbContext.Products
                                   .AnyAsync(
                                        p => auxIdProducts.Contains(p.ProductId)
                                        && !p.RestaurantId.Equals(order.RestaurantId));

            if (badRestaurantProductRelationship)
            {
                throw new Exception("Some products in the order, do not correspond to the restaurant used in the order");
            }

            await dbContext.OrderProducts.AddRangeAsync(newOrderProducts);
            await dbContext.SaveChangesAsync();
        }

        public async Task<CoordinateDto> GetOrderCoordinateAsync(string trackingNumber)
        {
            string? coordinate;
            var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.TrackingNumber.Equals(trackingNumber));

            if (order == null)
            {
                throw new Exception($"No Order with the TrackingNumber {trackingNumber} was found");
            }

            if (order.OrderStatus.Equals(OrderStatus.Delivered))
            {
                throw new Exception($"The Order with the TrackingNumber {trackingNumber} was delivered");
            }

            if (order.OrderStatus.Equals(OrderStatus.OnTheWay))
            {
                if (order.DeliveryVehicleId == null)
                {
                    throw new Exception("The Order has On The Way status but does not have a Delivery Vehicle assigned");
                }

                coordinate = await (
                                        from v in dbContext.DeliveryVehicles
                                        where v.DeliveryVehicleId.Equals(order.DeliveryVehicleId)
                                        select v.Coordinate
                                    )
                                    .FirstOrDefaultAsync<string>();
            }
            else
            {
                coordinate = await (
                                       from r in dbContext.Restaurants
                                       where r.RestaurantId.Equals(order.RestaurantId)
                                       select r.Coordinate
                                   )
                                   .FirstOrDefaultAsync<string>();
            }

            return new CoordinateDto(coordinate ?? string.Empty);
        }
    }
}
