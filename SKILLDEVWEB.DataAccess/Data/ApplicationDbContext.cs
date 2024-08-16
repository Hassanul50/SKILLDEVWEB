using Microsoft.EntityFrameworkCore;
using SKILLDEVWEB.Model.Models;

namespace SKILLDEVWEB.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, CategoryName = "Scifi", DisplayOrder = 2 },
                new Category { CategoryId = 3, CategoryName = "History", DisplayOrder = 3 },
                new Category { CategoryId = 4, CategoryName = "Cartoon", DisplayOrder = 4 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Title = "ABC",
                    Author = "ABS",
                    Description = "TEST",
                    ISBN = "ABC-123",
                    ListPrice = 30,
                    Price = 25,
                    Price50 = 20,
                    Price100 = 18
                },
                new Product
                {
                    ProductId = 2,
                    Title = "TEST-2",
                    Author = "ABS-2",
                    Description = "TEST-2",
                    ISBN = "ABC-645",
                    ListPrice = 54,
                    Price = 47,
                    Price50 = 32,
                    Price100 = 20
                },
                new Product
                {
                    ProductId = 3,
                    Title = "ABC-3",
                    Author = "ABS-3",
                    Description = "TEST-3",
                    ISBN = "ABC-1485",
                    ListPrice = 301,
                    Price = 252,
                    Price50 = 208,
                    Price100 = 180
                }

                );
        }
    }
}
