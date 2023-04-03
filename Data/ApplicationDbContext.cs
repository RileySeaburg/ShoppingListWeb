using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shopping_List.Models;
using System.IO;

namespace ShoppingListWeb1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSet for ShoppingItem
        public DbSet<ShoppingItem> Shopping_List { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base method to handle Identity schema
            modelBuilder.Entity<ShoppingItem>().ToTable(nameof(Shopping_List));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("ShoppingDatabase");

                // Use SQL Server as the database provider and set the connection string
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public string GetConnectionString()
        {
            return Database.GetConnectionString();
        }
    }
}
