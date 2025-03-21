using FinanceTrackerApp.Domain.Patterns.Visitor;

namespace FinanceTrackerApp.Domain.Entities;

public class Operation: IEntityVisitable, IStorable
{
    public Guid Id { get; private set; }
    public OperationType Type { get; private set; }
    public BankAccount? BankAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string? Description { get; set; }
    public Category? CategoryId { get; private set; }

    public Operation(OperationType type, BankAccount bankAccount, decimal amount, DateTime date,
        string? description = null, Category? categoryId = null)
    {
        Id = Guid.NewGuid();
        Type = type;
        BankAccountId = bankAccount;
        Amount = amount;
        Date = date;
        Description = description;
        CategoryId = categoryId;
    }
    
    public void Accept(IEntityVisitor visitor)
    {
        visitor.Visit(this);
    }
}