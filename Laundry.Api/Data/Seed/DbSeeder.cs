using Laundry.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Laundry.Api.Data.Seed
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Vendors
            modelBuilder.Entity<Vendor>().HasData(
                new Vendor { Id = 1, Name = "Sparkle Laundry", Description = "Fast & reliable laundry service", Phone = "1234567890", Email = "contact@sparklelaundry.com", Address = "123 Main St", Latitude = 40.7128, Longitude = -74.0060, IsActive = true, AverageRating = 4.5, TotalReviews = 2 },
                new Vendor { Id = 2, Name = "Fresh Wash", Description = "Eco-friendly washing", Phone = "0987654321", Email = "info@freshwash.com", Address = "456 Elm St", Latitude = 34.0522, Longitude = -118.2437, IsActive = true, AverageRating = 4.7, TotalReviews = 3 }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "Super Admin", Email = "superadmin@laundry.com", PasswordHash = "hashedpassword", Role = "SuperAdmin" },
                new User { Id = 2, FullName = "Vendor Admin Sparkle", Email = "admin@sparklelaundry.com", PasswordHash = "hashedpassword", Role = "VendorAdmin", VendorId = 1 },
                new User { Id = 3, FullName = "Vendor Employee 1", Email = "employee1@sparklelaundry.com", PasswordHash = "hashedpassword", Role = "Employee", VendorId = 1 },
                new User { Id = 4, FullName = "John Doe", Email = "john.doe@example.com", PasswordHash = "hashedpassword", Role = "Customer" },
                new User { Id = 5, FullName = "Jane Smith", Email = "jane.smith@example.com", PasswordHash = "hashedpassword", Role = "Customer" }
            );

            // Seed Services (fixed PricePerKg)
            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "Wash & Fold", Description = "Basic wash and fold service", PricePerKg = 5.00m, VendorId = 1 },
                new Service { Id = 2, Name = "Dry Cleaning", Description = "Premium dry cleaning", PricePerKg = 15.00m, VendorId = 1 },
                new Service { Id = 3, Name = "Wash & Iron", Description = "Wash and iron clothes", PricePerKg = 10.00m, VendorId = 2 }
            );

            // Seed Orders (fixed DateTimes)
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, CustomerId = 4, VendorId = 1, CreatedAt = new DateTime(2025, 6, 9), PickupDate = new DateTime(2025, 6, 10), DeliveryDate = new DateTime(2025, 6, 12), Status = "Completed" },
                new Order { Id = 2, CustomerId = 5, VendorId = 2, CreatedAt = new DateTime(2025, 6, 11), PickupDate = new DateTime(2025, 6, 12), Status = "Pending" }
            );

            // Seed OrderItems (fixed QuantityKg)
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, ServiceId = 1, QuantityKg = 3, UnitPrice = 5.00m },
                new OrderItem { Id = 2, OrderId = 1, ServiceId = 2, QuantityKg = 1, UnitPrice = 15.00m },
                new OrderItem { Id = 3, OrderId = 2, ServiceId = 3, QuantityKg = 2, UnitPrice = 10.00m }
            );

            // Seed Reviews (fixed DateTimes)
            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, CustomerId = 4, VendorId = 1, Rating = 5, Comment = "Great service!", CreatedAt = new DateTime(2025, 6, 10) },
                new Review { Id = 2, CustomerId = 5, VendorId = 2, Rating = 4, Comment = "Good but room for improvement.", CreatedAt = new DateTime(2025, 6, 11) }
            );

            // Seed VendorInquiries (fixed SentAt property and DateTimes)
            modelBuilder.Entity<VendorInquiry>().HasData(
                new VendorInquiry { Id = 1, CustomerId = 4, VendorId = 1, Message = "Do you offer express service?", SentAt = new DateTime(2025, 6, 8), IsResponded = false },
                new VendorInquiry { Id = 2, CustomerId = 5, VendorId = 2, Message = "Can I schedule a pickup on weekends?", SentAt = new DateTime(2025, 6, 9), IsResponded = false }
            );
        }
    }
}
