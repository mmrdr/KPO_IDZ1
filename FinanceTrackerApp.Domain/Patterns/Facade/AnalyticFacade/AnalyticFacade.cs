using FinanceTrackerApp.Domain.AnalyticService;
using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Patterns.Facade.AnalyticFacade;

public class AnalyticFacade: IAnalyticFacade
{
    private readonly IAnalytics _analytics;

    public AnalyticFacade(IAnalytics analytics)
    {
        _analytics = analytics;
    }
    
    public decimal CalculateIncomeExpenseDifference(DateTime start, DateTime end)
    {
        return _analytics.CalculateIncomeExpenseDifference(start, end);
    }

    public Dictionary<OperationType, decimal> GroupByCategory(DateTime startDate, DateTime endDate)
    {
        return _analytics.GroupByCategory(startDate, endDate);
    }

    public Dictionary<OperationType, decimal> GetTotalIncomeAndExpense(DateTime startDate, DateTime endDate)
    {
        return _analytics.GetTotalIncomeAndExpense(startDate, endDate);
    }
}