using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace FinanceTrackerApp.Domain.Db;


public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FinanceAppDbContext>
{
    public FinanceAppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("AmazingConnectionString");
        var optionsBuilder = new DbContextOptionsBuilder<FinanceAppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new FinanceAppDbContext(optionsBuilder.Options);
    }
}