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
        public async Task<ActionResult> UpdateDeliveryVehicleCoordinateAsync(string deliveryVehicleId, Coordinate coordinate)
        {

            if (string.IsNullOrEmpty(deliveryVehicleId))
            {
                throw new ArgumentException("Invalid Vehicle Identification");
            }

            if (coordinate == null)
            {
                return BadRequest("Invalid Payload");
            }

            await vehiclesService.UpdateDeliveryVehicleCoordinateAsync(deliveryVehicleId, coordinate);
            return Ok();
        }

        [HttpGet("{deliveryVehicleId}/coordinate")]
        public async Task<ActionResult<Coordinate>> GetDeliveryVehicleCoordinateAsync(string deliveryVehicleId)
        {
            if (string.IsNullOrEmpty(deliveryVehicleId))
            {
                throw new ArgumentException("Invalid Vehicle Identification");
            }

            Coordinate coordinate = await vehiclesService.GetDeliveryVehicleCoordinateAsync(deliveryVehicleId);

            if (coordinate == null)
            {
                return BadRequest($"No Coordinate found for the Vehicle Identification {deliveryVehicleId}");
            }

            return Ok(coordinate);
        }

        [HttpPost("{deliveryVehicleId}/orders/{orderId}")]
        public async Task<ActionResult> CreateDeliveryVehicleOrderAsync(string deliveryVehicleId, string orderId)
        {
            if (string.IsNullOrEmpty(deliveryVehicleId))
            {
                throw new ArgumentException("Invalid Vehicle Identification");
            } 
            
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("Invalid Order Identification");
            }

            await vehiclesService.CreateDeliveryVehicleOrderAsync(deliveryVehicleId, orderId);
            return Ok();
        }

        [HttpDelete("{deliveryVehicleId}/orders/{orderId}")]
        public async Task<ActionResult> DeleteDeliveryVehicleOrderAsync(string deliveryVehicleId, string orderId)
        {
            if (string.IsNullOrEmpty(deliveryVehicleId))
            {
                throw new ArgumentException("Invalid Vehicle Identification");
            }

            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("Invalid Order Identification");
            }

            await vehiclesService.DeleteDeliveryVehicleOrderAsync(deliveryVehicleId, orderId);
            return Ok();
        }
    }
}
