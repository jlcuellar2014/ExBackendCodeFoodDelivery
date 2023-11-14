using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryAPI.Model
{
    public interface IFoodDeliveryContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<DeliveryVehicleCoordinates> DeliveryVehicleCoordinates { get; set; }
        DbSet<DeliveryVehicle> DeliveryVehicles { get; set; }
        DbSet<OrderProduct> OrderProducts { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Restaurant> Restaurants { get; set; }

        Task SaveChangesAsync();
    }
}