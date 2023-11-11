using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryAPI.Model
{
    public class FoodDeliveryContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryVehicle> DeliveryVehicles { get; set; }
        public DbSet<DeliveryVehicleCoordinates> DeliveryVehicleCoordinates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  I'm sure that is not the way to do it :)
            optionsBuilder.UseSqlite($"Data Source=FoodDelivery.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasKey(o => new { o.OrderId, o.ProductId });
        }
    }
}
