using FoodDeliveryAPI.Dtos;

namespace FoodDeliveryAPI.Services
{
    public class OrdersService : IOrdersService
    {
        public async Task CreateOrderAsync(CreateOrderDto order)
        {
            await Task.Delay(100);
        }

        public async Task<CoordinateDto> GetOrderCoordinateAsync(string trackingNumber)
        {
            await Task.Delay(100);
            return new CoordinateDto { Latitude = 10.10d, Longitude = 12.74d };
        }
    }
}
