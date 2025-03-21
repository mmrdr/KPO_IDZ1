using FinanceTrackerApp.Domain.Db;
using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.AnalyticService;

public class Analytics: IAnalytics
{
    private readonly FinanceAppDbContext _context;

    public Analytics(FinanceAppDbContext context)
    {
        _context = context;
    }
    
    public decimal CalculateIncomeExpenseDifference(DateTime start, DateTime end)
    {
        var incomes = _context.Operations
            .Where(o => o.Type == OperationType.Income && o.Date >= start && o.Date <= end)
            .Sum(o => o.Amount);
        var expenses = _context.Operations
            .Where(o => o.Type == OperationType.Expense && o.Date >= start && o.Date <= end)
            .Sum(o => o.Amount);
        return incomes - expenses;
    }

    public Dictionary<OperationType, decimal> GroupByCategory(DateTime startDate, DateTime endDate)
    {
        var result = _context.Operations
            .Where(o => o.Date >= startDate && o.Date <= endDate)
            .GroupBy(o => o.Type == OperationType.Income ? OperationType.Income : OperationType.Expense)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(o => o.Amount)
            );
        return result;
    }
    public Dictionary<OperationType, decimal> GetTotalIncomeAndExpense(DateTime startDate, DateTime endDate)
    {
        var incomes = _context.Operations
            .Where(o => o.Type == OperationType.Income && o.Date >= startDate && o.Date <= endDate)
            .Sum(o => o.Amount);

        var expenses = _context.Operations
            .Where(o => o.Type == OperationType.Expense && o.Date >= startDate && o.Date <= endDate)
            .Sum(o => o.Amount);
        return new Dictionary<OperationType, decimal>
        {
            { OperationType.Income, incomes },
            { OperationType.Expense, expenses }
        };
    }
}