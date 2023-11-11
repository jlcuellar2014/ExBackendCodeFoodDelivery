using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryAPI.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly FoodDeliveryContext dbContext;
        public OrdersService()
        {
            dbContext = new FoodDeliveryContext();
        }

        public async Task CreateOrderAsync(CreateOrderDto order)
        {
            await Task.Delay(100);
        }

        public async Task<CoordinateDto> GetOrderCoordinateAsync(string trackingNumber)
        {
            string? coordinate;
            var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.TrackingNumber.Equals(trackingNumber));

            // FIXME - Make a better use of the exceptions here!

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

            if (coordinate == null)
            {
                throw new Exception("The Order no has Coordinate assigned");
            }

            string[] aux = coordinate.Split(',');

            return new CoordinateDto
            {
                Latitude = double.TryParse(aux[0].Trim(), out double latitude) ? latitude : 0,
                Longitude = double.TryParse(aux[1].Trim(), out double longitude) ? longitude : 0
            };
        }
    }
}
