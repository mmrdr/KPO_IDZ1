using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Patterns.Factory;

public class BankAccountFactory: IBankAccountFactory
{
    public BankAccount Create(string name, decimal balance)
    {
        if (balance < 0)
        {
            throw new ArgumentException("Balance cannot be negative");
        }
        return new BankAccount(name, balance);
    }
}