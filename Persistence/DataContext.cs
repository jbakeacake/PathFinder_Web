using Domain;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<User> Users_Tbl { get; set; }
        public DbSet<Player> Player_Tbl { get; set; }
        public DbSet<ItemData> Items_Tbl { get; set; }
        public DbSet<ItemType> ItemType_Tbl { get; set; }
        public DbSet<Potion> Potion_Tbl { get; set; }
        public DbSet<Shield> Shield_Tbl { get; set; }
        public DbSet<Weapon> Weapon_Tbl { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>();
            builder.Entity<Player>();

            builder.Entity<ItemData>();
            builder.Entity<ItemType>();
            builder.Entity<Potion>();
            builder.Entity<Shield>();
            builder.Entity<Weapon>();
        }
    }
}