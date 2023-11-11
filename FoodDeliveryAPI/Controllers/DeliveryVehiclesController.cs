using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryVehiclesController : ControllerBase
    {
        private readonly IDeliveryVehiclesService vehiclesService;

        public DeliveryVehiclesController(IDeliveryVehiclesService vehiclesService)
        {
            this.vehiclesService = vehiclesService;
        }

        [HttpPatch("{deliveryVehicleId}/coordinate")]
        public async Task<ActionResult> UpdateDeliveryVehicleCoordinateAsync(string deliveryVehicleId, CoordinateDto coordinate)
        {
            await vehiclesService.UpdateDeliveryVehicleCoordinateAsync(deliveryVehicleId, coordinate);
            return Ok();
        }

        [HttpGet("{deliveryVehicleId}/coordinate")]
        public async Task<ActionResult<CoordinateDto>> GetDeliveryVehicleCoordinateAsync(string deliveryVehicleId)
        {
            CoordinateDto coordinate = await vehiclesService.GetDeliveryVehicleCoordinateAsync(deliveryVehicleId);
            return Ok(coordinate);
        }

        [HttpPost("{deliveryVehicleId}/orders/{orderId}")]
        public async Task<ActionResult> CreateDeliveryVehicleOrderAsync(string deliveryVehicleId, string orderId)
        {
            await vehiclesService.CreateDeliveryVehicleOrderAsync(deliveryVehicleId.ToLower(), orderId);
            return Ok();
        }

        [HttpDelete("{deliveryVehicleId}/orders/{orderId}")]
        public async Task<ActionResult> DeleteDeliveryVehicleOrderAsync(string deliveryVehicleId, string orderId)
        {
            await vehiclesService.DeleteDeliveryVehicleOrderAsync(deliveryVehicleId, orderId);
            return Ok();
        }
    }
}
