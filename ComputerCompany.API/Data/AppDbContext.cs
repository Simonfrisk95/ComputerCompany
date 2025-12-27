using ComputerCompany.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerCompany.API.Data;

// Database context responsible for persisting orders, products and stock movements
// for the ComputerCompany warehouse and order management system
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Computers and hardware components stored in the warehouse
    public DbSet<Article> Articles => Set<Article>();

    // Customer orders placed through the internal system
    public DbSet<Order> Orders => Set<Order>();

    // Individual order rows connecting products to orders
    public DbSet<OrderRow> OrderRows => Set<OrderRow>();

    // Inbound deliveries used to increase warehouse stock levels
    public DbSet<InboundDelivery> InboundDeliveries => Set<InboundDelivery>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configures the relationship between orders and their order rows
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderRows)
            .WithOne(r => r.Order)
            .HasForeignKey(r => r.OrderId);

        // Configures the relationship between articles and order rows
        modelBuilder.Entity<Article>()
            .HasMany(a => a.OrderRows)
            .WithOne(r => r.Article)
            .HasForeignKey(r => r.ArticleId);

        // Ensures that each SKU is unique across all products in the warehouse
        modelBuilder.Entity<Article>()
            .HasIndex(a => a.SKU)
            .IsUnique();
    }
}
