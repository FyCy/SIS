using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class DemoDbContex : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products {get;set;}

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProducts> OrderProducts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-C17BCEM\\SQLEXPRESS;Database=WebAppDemo;Integrated Security=True;").UseLazyLoadingProxies();
        }
    }
}
