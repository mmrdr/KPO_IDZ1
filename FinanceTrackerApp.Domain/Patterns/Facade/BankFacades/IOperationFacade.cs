using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Abstractions.Facade;

namespace FinanceTrackerApp.Domain.Patterns.Facade;

public interface IOperationFacade: IFacade<Operation>
{
    Operation CreateOperationById(OperationType type, Guid bankAccountId, decimal amount, string? description,
        Guid? categoryId);
    void CreateFromFile(Operation operation);

}