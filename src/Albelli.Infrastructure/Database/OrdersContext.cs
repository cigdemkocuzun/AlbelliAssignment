using Microsoft.EntityFrameworkCore;
using Albelli.Domain.Customers;
using Albelli.Domain.Products;
using Albelli.Infrastructure.Processing.InternalCommands;
using Albelli.Infrastructure.Processing.Outbox;

namespace Albelli.Infrastructure.Database
{
    public class OrdersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InternalCommand> InternalCommands { get; set; }
        public OrdersContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
        }
    }
}
