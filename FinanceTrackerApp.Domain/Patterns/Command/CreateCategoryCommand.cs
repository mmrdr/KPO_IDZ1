using FinanceTrackerApp.Domain.Abstractions.Command;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Patterns.Facade;

namespace FinanceTrackerApp.Domain.Patterns.Command;

public class CreateCategoryCommand: ICommand
{
    private readonly ICategoryFacade _categoryFacade;
    private string _name;
    private OperationType _type;
    
    public CreateCategoryCommand(ICategoryFacade categoryFacade, string name, OperationType type)
    {
        _categoryFacade = categoryFacade;
        _name = name;
        _type = type;
    }
    
    public void Execute() => _categoryFacade.CreateCategory(_type, _name);
}