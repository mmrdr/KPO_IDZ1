using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Patterns.Factory;

public interface IBankAccountFactory
{
    public BankAccount Create(string name, decimal balance);
}