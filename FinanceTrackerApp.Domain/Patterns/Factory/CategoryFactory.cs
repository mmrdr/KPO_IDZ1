using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Patterns.Factory;

public class CategoryFactory: ICategoryFactory  
{
    public Category Create(OperationType type, string name)
    {
        return new Category(type, name);
    }
}