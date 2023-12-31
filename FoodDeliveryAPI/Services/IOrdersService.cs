﻿using FoodDeliveryAPI.Dtos;

namespace FoodDeliveryAPI.Services
{
    public interface IOrdersService
    {
        Task CreateOrderAsync(CreateOrderDto order);
        Task<CoordinateDto> GetOrderCoordinateAsync(string trackingNumber);
    }
}