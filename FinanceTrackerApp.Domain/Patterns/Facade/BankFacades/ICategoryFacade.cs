using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Abstractions.Facade;

namespace FinanceTrackerApp.Domain.Patterns.Facade;

public interface ICategoryFacade: IFacade<Category>
{
    Category CreateCategory(OperationType type, string name);
    void CreateFromFile(Category category);
    void ChangeName(Guid id, string newName);
}