using BankEfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
namespace BankEfCore;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder
            .UseSqlServer("Server=localhost\\SQLEXPRESS; Database=EFCoreBank; Trusted_Connection=True; TrustServerCertificate=True")
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();



        // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // AccountType To String 
        modelBuilder.Entity<Account>()
            .Property(a => a.AccountType)
            .HasConversion<string>();

        // OwnershipType To String  
        modelBuilder.Entity<CustomerAccount>()
            .Property(ca => ca.OwnershipType)
            .HasConversion<string>();

        // CustomerType To String 
        modelBuilder.Entity<Customer>()
            .Property(c => c.CustomerType)
            .HasConversion<string>();


        // 1 - 1 Branch And Manager
        modelBuilder.Entity<Branch>()
            .HasOne(b => b.Manager)
            .WithOne(m => m.Branch)
            .HasForeignKey<Branch>(m => m.ManagerId);


        // 1 - M Account And Transaction
        modelBuilder.Entity<Account>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(a => a.AccountNumber);


        // 1 - M Branch and Account
        modelBuilder.Entity<Branch>()
            .HasMany(b => b.Accounts)
            .WithOne(a => a.Branch)
            .HasForeignKey(a => a.BranchCode);

        // M - M Customer And Account
        //1- Keys
        modelBuilder.Entity<CustomerAccount>()
            .HasKey(ca => new
            {
                ca.CustomerId,
                ca.AccountNumber
            });
        //2- Relations
        modelBuilder.Entity<CustomerAccount>()
            .HasOne(ca => ca.Customer)
            .WithMany(c => c.CustomerAccounts)
            .HasForeignKey(ca => ca.CustomerId);

        modelBuilder.Entity<CustomerAccount>()
            .HasOne(ca => ca.Account)
            .WithMany(c => c.CustomerAccounts)
            .HasForeignKey(ca => ca.AccountNumber);
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Branch> Branchs { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<CustomerAccount> CustomerAccounts { get; set; }


}
