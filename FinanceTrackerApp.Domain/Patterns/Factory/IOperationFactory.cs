using System.ComponentModel;
using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Patterns.Factory;

public interface IOperationFactory
{
    public Operation Create(OperationType type, Guid? bankAccount, decimal amount, DateTime date, string? description,
        Guid? category);
}