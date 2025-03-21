using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Patterns.Factory;

public interface ICategoryFactory
{
    public Category Create(OperationType type, string name);
}