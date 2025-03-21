using Microsoft.EntityFrameworkCore;

namespace JwtAuthApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    Password = "admin123", // In a real app, this should be hashed
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    Email = "user@example.com",
                    Password = "user123", // In a real app, this should be hashed
                    Role = "User"
                }
            );
        }
    }
} 