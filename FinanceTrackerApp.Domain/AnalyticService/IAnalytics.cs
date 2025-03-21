using FinanceTrackerApp.Domain.Entities;
namespace FinanceTrackerApp.Domain.Service;

public interface IAnalytics
{
    public decimal CalculateIncomeExpenseDifference(DateTime start, DateTime end);
    public Dictionary<OperationType, decimal> GroupByCategory(DateTime startDate, DateTime endDate);
    public Dictionary<OperationType, decimal> GetTotalIncomeAndExpense(DateTime startDate, DateTime endDate);

}