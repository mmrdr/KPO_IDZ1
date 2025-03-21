using FinanceTrackerApp.Domain.Abstractions.Command;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Patterns.Facade;
namespace FinanceTrackerApp.Domain.Patterns.Command;

public class CreateOperationCommand: ICommand
{
    private readonly IOperationFacade _operationFacade;
    private OperationType _operationType;
    private Guid _bankAccountId;
    private decimal _amount;
    private DateTime _date;
    private string _description;
    private Guid? _categoryId;
    
    public CreateOperationCommand
    (
        IOperationFacade operationFacade, 
        OperationType operationType, 
        Guid bankAccountId, 
        decimal amount, 
        DateTime date, 
        string description, 
        Guid? categoryId
        )
    {
        _operationFacade = operationFacade;
        _operationType = operationType;
        _bankAccountId = bankAccountId;
        _amount = amount;
        _date = date;
        _description = description;
        _categoryId = categoryId;        
    }
    public void Execute() => _operationFacade.CreateOperationById(_operationType, _bankAccountId, _amount,  _description, _categoryId);
}