using Laundry.Api.Data.Seed;
using Laundry.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Laundry.Api.Data
{
    public class LaundryDbContext : DbContext
    {
        public LaundryDbContext(DbContextOptions<LaundryDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Vendor> Vendors { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<VendorInquiry> VendorInquiries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique Email for User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.Vendor)
                .WithMany(v => v.Users)
                .HasForeignKey(u => u.VendorId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.Vendor)
                .WithMany(v => v.Services)
                .HasForeignKey(s => s.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Vendor)
                .WithMany(v => v.Orders)
                .HasForeignKey(o => o.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VendorInquiry>()
                .HasOne(vi => vi.Vendor)
                .WithMany(v => v.Inquiries)
                .HasForeignKey(vi => vi.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VendorInquiry>()
                .HasOne(vi => vi.Customer)
                .WithMany()
                .HasForeignKey(vi => vi.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Vendor)
                .WithMany(v => v.Reviews)
                .HasForeignKey(r => r.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Service)
                .WithMany()
                .HasForeignKey(oi => oi.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Constraints for OrderItem
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Quantity)
                .IsRequired();

            // Constraints and property configs for other entities
            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Vendor>()
                .Property(v => v.Name)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Service>()
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Review>()
                .Property(r => r.Comment)
                .HasMaxLength(1000);

            // Seed data
            modelBuilder.Seed();
        }
    }
}
