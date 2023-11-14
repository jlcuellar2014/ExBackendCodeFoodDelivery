using FoodDeliveryAPI.Dtos;

namespace FoodDeliveryTests.Utilities
{
    public static class CoordinateDtoExt
    {
        public static string ToPlainFormat(this CoordinateDto coordinate) 
            => $"{coordinate.Latitude},{coordinate.Longitude}";
    }
}
