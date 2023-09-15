using Microsoft.EntityFrameworkCore;
using SHIBANK.Models;

namespace SHIBANK.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User y BankAccount
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id)
                .HasName("PK_Users");

            modelBuilder.Entity<User>()
                .HasMany(u => u.BankAccounts)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            // BankAccount y Transaction
            modelBuilder.Entity<BankAccount>()
               .HasKey(u => u.Id)
               .HasName("PK_BankAccount");

            modelBuilder.Entity<BankAccount>()
                .HasMany(b => b.Transactions)
                .WithOne(t => t.BankAccount)
                .HasForeignKey(t => t.BankAccountId);

            modelBuilder.Entity<BankAccount>()
                .Property(b => b.Balance)
                .HasColumnType("decimal(18, 2)");


            modelBuilder.Entity<Transaction>()
               .HasKey(u => u.Id)
               .HasName("PK_Transaction");
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18, 2)"); 

        }

    }
}
