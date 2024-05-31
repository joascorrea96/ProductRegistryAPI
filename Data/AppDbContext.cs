using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductRegistryAPI.Models;

namespace ProductRegistryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Description, p.Brand })
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(S => S.Products)
                .HasForeignKey(p => p.SupplierId);
 

            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.CNPJ)
                .IsUnique();
        }

        public async Task<List<Product>> GetProductsBySupplierAsync(int supplierId)
        {
            return await Products
                .FromSqlRaw("EXEC GetProductsBySupplier @p0", supplierId)
                .ToListAsync();
        }


    }
}
