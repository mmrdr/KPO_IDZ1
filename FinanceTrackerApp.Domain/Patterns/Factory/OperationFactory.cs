using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Patterns.Factory;

public class OperationFactory: IOperationFactory
{
    public Operation Create(OperationType type, Guid? bankAccount, decimal amount, string? description,
        Guid? category)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative");
        }
        return new Operation(type, bankAccount, amount, description, category);
    }
}