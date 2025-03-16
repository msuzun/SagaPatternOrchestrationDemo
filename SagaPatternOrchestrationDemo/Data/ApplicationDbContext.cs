using Microsoft.EntityFrameworkCore;
using SagaPatternOrchestrationDemo.Model;

namespace SagaPatternOrchestrationDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
    }
}
