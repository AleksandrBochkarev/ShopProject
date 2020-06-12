using System;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRole, int>

    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryProducts> CategoryProducts { get; set; }
        public DbSet<UserOrders> UserOrders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> User { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }

    }

}