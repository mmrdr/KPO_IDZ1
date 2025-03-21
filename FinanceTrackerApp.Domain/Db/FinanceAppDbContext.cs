using Microsoft.EntityFrameworkCore;
using FinanceTrackerApp.Domain.Entities;
namespace FinanceTrackerApp.Domain.Db;

public class FinanceAppDbContext: DbContext
{
    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Operation> Operations => Set<Operation>();
    
    public FinanceAppDbContext(DbContextOptions<FinanceAppDbContext> options)
        : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=financedb;Username=postgres;Password=secret");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.ToTable("bank_accounts");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Balance).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.ToTable("operations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(255);
            
            entity.HasOne<BankAccount>()
                .WithMany()
                .HasForeignKey(e => e.BankAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Category>()
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}