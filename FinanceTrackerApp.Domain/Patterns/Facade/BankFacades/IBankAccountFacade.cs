using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Abstractions.Facade;

namespace FinanceTrackerApp.Domain.Patterns.Facade;

public interface IBankAccountFacade: IFacade<BankAccount>
{
    BankAccount CreateAccount(string name, decimal balance);
    void CreateFromFile(BankAccount account);
    void ChangeName(Guid id, string name);
    void IncreaseBalance(Guid id, decimal amount);
    void DecreaseBalance(Guid id, decimal amount);
}