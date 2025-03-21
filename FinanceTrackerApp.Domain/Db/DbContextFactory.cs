using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace FinanceTrackerApp.Domain.Db;


public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FinanceAppDbContext>
{
    public FinanceAppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FinanceAppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=financedb;Username=postgres;Password=secret");
        return new FinanceAppDbContext(optionsBuilder.Options);
    }
}