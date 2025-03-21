using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Abstractions.Facade;

namespace FinanceTrackerApp.Domain.Patterns.Facade;

public interface IOperationFacade: IFacade<Operation>
{
    Operation CreateOperationById(OperationType type, Guid bankAccountId, decimal amount, DateTime date, string? description,
        Guid? categoryId);

}