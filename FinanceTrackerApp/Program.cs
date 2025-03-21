using FinanceTrackerApp.Domain.Abstractions.Export;
using FinanceTrackerApp.Domain.Abstractions.Import;
using FinanceTrackerApp.Domain.AnalyticService;
using FinanceTrackerApp.Domain.Patterns.Facade;
using FinanceTrackerApp.Domain.Patterns.Facade.AnalyticFacade;
using FinanceTrackerApp.Domain.Patterns.Proxy;
using FinanceTrackerApp.Domain.Repository;
using FinanceTrackerApp.Domain.Db;
using FinanceTrackerApp.Domain.Export;
using FinanceTrackerApp.Domain.Import;
using FinanceTrackerApp.Domain.Patterns.Facade.DataTransferFacade;
using FinanceTrackerApp.Domain.Patterns.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;namespace FinanceTrackerApp;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddDbContext<FinanceAppDbContext>(options =>
            options.UseNpgsql("Host=localhost;Port=5432;Database=financedb;Username=postgres;Password=secret"));
        
        services.AddScoped<BankAccountRepository>();
        services.AddScoped<CategoryRepository>();
        services.AddScoped<OperationRepository>();
        
        services.AddScoped<IBankAccountRepository>(sp =>
            new BankRepositoryProxy(sp.GetRequiredService<BankAccountRepository>()));
        services.AddScoped<ICategoryRepository>(sp =>
            new CategoryRepositoryProxy(sp.GetRequiredService<CategoryRepository>()));
        services.AddScoped<IOperationRepository>(sp =>
            new OperationRepositoryProxy(sp.GetRequiredService<OperationRepository>()));
        
        services.AddScoped<IBankAccountFactory, BankAccountFactory>();
        services.AddScoped<ICategoryFactory, CategoryFactory>();
        services.AddScoped<IOperationFactory, OperationFactory>();
        
        services.AddScoped<IAnalytics, Analytics>();
        services.AddScoped<DataImporter, JsonDataImporter>();
        services.AddScoped<DataImporter, YamlDataImporter>();
        services.AddScoped<DataImporter, CsvDataImporter>();

        services.AddScoped<DataExporter, JsonExporter>();
        services.AddScoped<DataExporter, YamlExporter>();
        services.AddScoped<DataExporter, CsvExporter>();
        
        services.AddScoped<IBankAccountFacade, BankAccountFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<IOperationFacade, OperationFacade>();
        services.AddScoped<IAnalyticFacade, AnalyticFacade>();
        services.AddScoped<IDataTransferFacade, DataTransferFacade>();
        
        services.AddScoped<UI>();

        var serviceProvider = services.BuildServiceProvider();
        
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<FinanceAppDbContext>();
            dbContext.Database.Migrate();

            var ui = scope.ServiceProvider.GetRequiredService<UI>();
            ui.RunApp();
        }
    }
}