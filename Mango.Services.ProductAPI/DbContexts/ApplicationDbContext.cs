using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Kebap",
                Price = 25,
                Description = "Kebap ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                ImageUrl = "https://mediastormango.blob.core.windows.net/mango/kebap.jpg",
                CategoryName = "Appetizer",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Iskender",
                Price = 34.99,
                Description = "Iskender ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                ImageUrl = "https://mediastormango.blob.core.windows.net/mango/iskender.jpg",
                CategoryName = "Appetizer",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Doner",
                Price = 15.99,
                Description = "Doner ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                ImageUrl = "https://mediastormango.blob.core.windows.net/mango/doner.jpg",
                CategoryName = "Appetizer",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Pide",
                Price = 13.85,
                Description = "Pide ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                ImageUrl = "https://mediastormango.blob.core.windows.net/mango/pide.jpg",
                CategoryName = "Appetizer",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "Baklava",
                Price = 10.35,
                Description = "Baklava ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                ImageUrl = "https://mediastormango.blob.core.windows.net/mango/baklava.jpg",
                CategoryName = "Dessert",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 6,
                Name = "Donut",
                Price = 8.88,
                Description = "Donut ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                ImageUrl = "https://mediastormango.blob.core.windows.net/mango/donut.jpg",
                CategoryName = "Dessert",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 7,
                Name = "Atom",
                Price = 8.88,
                Description = "Atom ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                ImageUrl = "https://mediastormango.blob.core.windows.net/mango/atom.jpg",
                CategoryName = "Entree",
            });
        }
    }
}
