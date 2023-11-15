using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [Authorize]
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
        public async Task<ActionResult> UpdateDeliveryVehicleCoordinateAsync(int deliveryVehicleId, CoordinateDto coordinate)
        {
            if (coordinate == null)
            {
                return BadRequest("Invalid Payload");
            }

            try
            {
                await vehiclesService.UpdateDeliveryVehicleCoordinateAsync(deliveryVehicleId, coordinate);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{deliveryVehicleId}/coordinate")]
        public async Task<ActionResult<CoordinateDto>> GetDeliveryVehicleCoordinateAsync(int deliveryVehicleId)
        {
            try
            {
                var coordinate = await vehiclesService.GetDeliveryVehicleCoordinateAsync(deliveryVehicleId);

                if (coordinate == null)
                {
                    return BadRequest($"No Coordinate found for the Vehicle Identification {deliveryVehicleId}");
                }

                return Ok(coordinate);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("{deliveryVehicleId}/orders/{orderId}")]
        public ActionResult CreateDeliveryVehicleOrder(int deliveryVehicleId, int orderId)
        {
            try
            {
                vehiclesService.CreateDeliveryVehicleOrder(deliveryVehicleId, orderId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{deliveryVehicleId}/orders/{orderId}")]
        public async Task<ActionResult> DeleteDeliveryVehicleOrderAsync(int deliveryVehicleId, int orderId)
        {
            try
            {
                await vehiclesService.DeleteDeliveryVehicleOrderAsync(deliveryVehicleId, orderId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
