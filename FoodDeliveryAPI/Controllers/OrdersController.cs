using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderAsync(CreateOrderDto order)
        {
            if (order == null)
            {
                return BadRequest("Invalid Payload");
            }

            try
            {
                await ordersService.CreateOrderAsync(order);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{trackingNumber}/coordinate")]
        public async Task<ActionResult<CoordinateDto>> GetOrderCoordinateAsync(string trackingNumber)
        {
            if (string.IsNullOrEmpty(trackingNumber))
            {
                throw new ArgumentException("Invalid Traking Number");
            }

            try
            {
                var coordinate = await ordersService.GetOrderCoordinateAsync(trackingNumber);
                return Ok(coordinate);
            }
            catch (Exception)
            {
                return BadRequest($"No Coordinate found for Traking Number {trackingNumber}");
            }
        }
    }
}
