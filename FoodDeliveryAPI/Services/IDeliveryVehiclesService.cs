using FoodDeliveryAPI.Dtos;

namespace FoodDeliveryAPI.Services
{
    public interface IDeliveryVehiclesService
    {
        Task CreateDeliveryVehicleOrderAsync(string deliveryVehicleId, string orderId);
        Task DeleteDeliveryVehicleOrderAsync(string deliveryVehicleId, string orderId);
        Task<CoordinateDto> GetDeliveryVehicleCoordinateAsync(string deliveryVehicleId);
        Task UpdateDeliveryVehicleCoordinateAsync(string deliveryVehicleId, CoordinateDto coordinate);
    }
}