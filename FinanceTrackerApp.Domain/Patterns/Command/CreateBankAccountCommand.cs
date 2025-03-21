using FinanceTrackerApp.Domain.Abstractions.Command;
using FinanceTrackerApp.Domain.Patterns.Facade;

namespace FinanceTrackerApp.Domain.Patterns.Command;

public class CreateBankAccountCommand: ICommand
{
    private readonly IBankAccountFacade _bankAccountFacade;
    private string _name;
    private decimal _balance;

    public CreateBankAccountCommand(IBankAccountFacade bankAccountFacade, string name, decimal balance)
    {
        _bankAccountFacade = bankAccountFacade;
        _name = name;
        _balance = balance;
    }
    
    public void Execute() => _bankAccountFacade.CreateAccount(_name, _balance);
}