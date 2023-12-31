﻿using FoodDeliveryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryTests.Model
{
    public class FakeDbContext : DbContext, IFoodDeliveryContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryVehicle> DeliveryVehicles { get; set; }
        public DbSet<DeliveryVehicleCoordinates> DeliveryVehicleCoordinates { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public override int SaveChanges() => base.SaveChanges();
        public async Task SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("FakeFoodDeliveryDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasKey(o => new { o.OrderId, o.ProductId });
        }

        public override void Dispose()
        {
            this.Database.EnsureDeleted();
            base.Dispose();
        }
    }
}
