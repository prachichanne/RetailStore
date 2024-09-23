using Microsoft.EntityFrameworkCore;
using RetailStore.Model;
using System;
using System.Diagnostics;

namespace RetailStore.Data
{
    public class RetailDBContext : DbContext
    {

        public RetailDBContext(DbContextOptions<RetailDBContext> options)
          : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the primary key and foreign key relationships
            modelBuilder.Entity<Product>()
             .HasOne(p => p.Category)
             .WithMany(c => c.Products)
             .HasForeignKey(p => p.CategoryId);

            
        }

    }
}
