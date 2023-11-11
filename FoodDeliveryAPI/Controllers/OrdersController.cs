﻿using FoodDeliveryAPI.Dtos;
using FoodDeliveryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
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

            await ordersService.CreateOrderAsync(order);
            return Ok();
        }

        [HttpGet("{trackingNumber}/coordinate")]
        public async Task<ActionResult<CoordinateDto>> GetOrderCoordinateAsync(string trackingNumber)
        {
            if (string.IsNullOrEmpty(trackingNumber))
            {
                throw new ArgumentException("Invalid Traking Number");
            }

            CoordinateDto coordinate = await ordersService.GetOrderCoordinateAsync(trackingNumber);

            if(coordinate == null)
            {
                return BadRequest($"No Coordinate found for the Traking Number {trackingNumber}");
            }

            return Ok(coordinate);
        }
    }
}