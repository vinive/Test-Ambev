using Microsoft.EntityFramworkCore;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(options) {

        }
        {        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Host=localhost;Port=5432;Database=test_ambev;Username=ambev;Password=ambev", 
                sqlOptions => sqlOptions.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM"));
        }
                public DbSet<Sale> Sales => Set<Sales>();
                public DbSet<SaleItem> SaleItems => Set<SaleItem>()
        }
    }
}