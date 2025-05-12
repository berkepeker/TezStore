using Microsoft.EntityFrameworkCore;
using TezStore.Models; // User sınıfını kullanmak için bu satırı ekleyin

namespace TezStore.Models
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<SellableItem> SellableItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RecyclingTransaction> RecyclingTransactions { get; set; }

        public DbSet<User> Users { get; set; } // User sınıfı burada tanımlanmış olmalı
       
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Transactions)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<SellableItem>()
                .HasMany(si => si.Transactions)
                .WithOne(t => t.SellableItem)
                .HasForeignKey(t => t.SellableItemId);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Transaction>()
                .Property(t => t.BalanceAfter)
                .HasPrecision(18, 2);
            modelBuilder.Entity<SellableItem>()
                .Property(si => si.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<User>()
                .Property(u => u.Balance)
                .HasPrecision(18, 2);
        }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
            : base(options)
        {
            this.Database.SetCommandTimeout(300); // Komut süresini artırabilirsiniz
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(); // Veritabanı hatalarını daha ayrıntılı gösterir
        }
    }
}