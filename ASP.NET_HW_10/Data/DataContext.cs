using ASP.NET_HW_9.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_HW_9.Data {
    public class DataContext : DbContext {
        public DbSet<User>? Users { get; set; }

        public DbSet<Product>? Products { get; set; }

        public DbSet<CartPosition>? CartPositions { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<CartPosition>()
                .HasKey(cp => cp.Id);

            modelBuilder.Entity<CartPosition>()
                .HasOne(cp => cp.User)
                .WithMany(u => u.CartPositions)
                .HasForeignKey(cp => cp.UserId);

            modelBuilder.Entity<CartPosition>()
                .HasOne(cp => cp.Product)
                .WithMany()
                .HasForeignKey(cp => cp.ProductId);
        }
    }
}