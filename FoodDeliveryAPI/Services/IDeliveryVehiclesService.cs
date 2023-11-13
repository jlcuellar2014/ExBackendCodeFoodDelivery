using FoodDeliveryAPI.Dtos;

namespace FoodDeliveryAPI.Services
{
    public interface IDeliveryVehiclesService
    {
        Task CreateDeliveryVehicleOrderAsync(int deliveryVehicleId, int orderId);
        Task DeleteDeliveryVehicleOrderAsync(int deliveryVehicleId, int orderId);
        Task<CoordinateDto> GetDeliveryVehicleCoordinateAsync(int deliveryVehicleId);
        Task UpdateDeliveryVehicleCoordinateAsync(int deliveryVehicleId, CoordinateDto coordinate);
    }
}