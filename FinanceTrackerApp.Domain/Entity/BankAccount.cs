using FinanceTrackerApp.Domain.Patterns.Visitor;
using CsvHelper.Configuration.Attributes;
namespace FinanceTrackerApp.Domain.Entities;

public class BankAccount: IEntityVisitable, IStorable
{
    [Name("Id")]
    public Guid Id { get; private set; }
    [Name("Name")]
    public string Name { get; private set; }
    [Name("Balance")]
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
        if (Balance - amount < 0)
        {
            throw new ArgumentOutOfRangeException("Balance cannot be negative, I will pay for you");
        }
        Balance -= amount;
    }

    public void Accept(IEntityVisitor visitor)
    { 
        visitor.Visit(this);
    }
}