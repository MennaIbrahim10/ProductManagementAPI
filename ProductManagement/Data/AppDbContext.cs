using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Electronic products"
                },
                new Category
                {
                    Id = 2,
                    Name = "Clothing",
                    Description = "Clothing products"
                },
                new Category
                {
                    Id = 3,
                    Name = "Books",
                    Description = "Books and novels"
                }

                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Description = "Gaming Laptop",
                    Price = 30000,
                    Stock = 10,
                    CreatedAt = new DateTime(2026, 8, 8),
                    CategoryId = 1
                },
                 new Product
                 {
                     Id = 2,
                     Name = "Phone",
                     Description = "Smart Phone",
                     Price = 15000,
                     Stock = 20,
                     CreatedAt = new DateTime(2026, 8, 8),
                     CategoryId = 1
                 },
                  new Product
                  {
                      Id = 3,
                      Name = "T-Shirt",
                      Description = "Cotton T-Shirt",
                      Price = 500,
                      Stock = 30,
                      CreatedAt = new DateTime(2026, 8, 8),
                      CategoryId = 2
                  }
                );

            modelBuilder.Entity<UserPermission>().HasKey(up => new { up.PermissionId, up.UserId });
        }

        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserPermission> userPermissions { get; set; }
    }
}
