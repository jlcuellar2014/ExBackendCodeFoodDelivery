using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Publishers;

namespace FoodDeliveryTests.Publishers
{
    public class FakePublisher : IMsgPublisher
    {
        public void PublishDeliveryVehicheMovedEvent(DeliveryVehicleMovedDto vehicleMovedDto)
        {
            
        }
    }
}
