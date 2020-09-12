using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {}

        public DbSet<User> Users_Tbl { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(); // Seed Data
        }
    }
}