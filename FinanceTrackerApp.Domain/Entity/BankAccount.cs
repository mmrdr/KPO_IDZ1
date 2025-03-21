using FinanceTrackerApp.Domain.Patterns.Visitor;

namespace FinanceTrackerApp.Domain.Entities;

public class BankAccount: IEntityVisitable, IStorable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Balance { get; set; }

    public BankAccount(string name, decimal balance)
    {
        Id = Guid.NewGuid();
        Name = name;    
        Balance = balance;
    }

    public void ChangeName(string newName)
    {
        Name = newName;
    }

    public void IncreaseBalance(decimal amount)
    {
        Balance += amount;
    }

    public void DecreaseBalance(decimal amount)
    {
        Balance -= amount;
    }

    public void Accept(IEntityVisitor visitor)
    { 
        visitor.Visit(this);
    }
}