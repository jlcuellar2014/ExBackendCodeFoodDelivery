using FoodDeliveryAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpPost]
        public ActionResult CreateOrder(CreateOrderDto order)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{trackingNumber}/coordinate")]
        public ActionResult<CoordinateDto> GetOrderCoordinate(string trackingNumber)
        {
            throw new NotImplementedException();
        }
    }
}
