using FoodDeliveryAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryVehiclesController : ControllerBase
    {
        [HttpPatch("{deliveryVehicleId}/coordinate")]
        public ActionResult UpdateDeliveryVehicleCoordinate(string deliveryVehicleId, CoordinateDto coordinate)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{deliveryVehicleId}/coordinate")]
        public ActionResult<CoordinateDto> GetDeliveryVehicleCoordinate(string deliveryVehicleId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{deliveryVehicleId}/orders/{orderId}")]
        public ActionResult CreateDeliveryVehicleOrder(string deliveryVehicleId, string orderId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{deliveryVehicleId}/orders/{orderId}")]
        public ActionResult DeleteDeliveryVehicleOrder(string deliveryVehicleId, string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
