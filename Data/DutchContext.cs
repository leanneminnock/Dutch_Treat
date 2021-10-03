using Dutch_Treat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dutch_Treat.Data
{
    // represents the database to the rest of the application.

    public class DutchContext : IdentityDbContext<StoreUser>
    {
        private readonly IConfiguration _config;

        public DutchContext(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DutchContextDb"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .Property(o => o.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Order>().HasData(new Order()
            {
                Id = 1,
                OrderDate = DateTime.Now,
                OrderNumber = "23453245"

            });
        }
    }
}
