using Microsoft.EntityFrameworkCore;

namespace OData.NetCore.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }

        public DatabaseContext() : base()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId);
        }
    }
}
