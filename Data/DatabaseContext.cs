using Apps.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apps.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(
        DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
