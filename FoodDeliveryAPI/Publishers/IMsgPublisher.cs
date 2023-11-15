using FoodDeliveryAPI.Dtos;

namespace FoodDeliveryAPI.Publishers
{
    public interface IMsgPublisher
    {
        void PublishDeliveryVehicheMovedEvent(DeliveryVehicleMovedDto vehicleMovedDto);
    }
}