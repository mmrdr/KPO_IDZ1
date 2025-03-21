using FinanceTrackerApp.Domain.Patterns.Visitor;

namespace FinanceTrackerApp.Domain.Entities;

public class Operation: IEntityVisitable, IStorable
{
    public Guid Id { get; private set; }
    public OperationType Type { get; private set; }
    public Guid? BankAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string? Description { get; private set; }
    public Guid? CategoryId { get; private set; }

    public Operation(OperationType type, Guid? bankAccountId, decimal amount, DateTime date,
        string? description = null, Guid? categoryId = null)
    {
        Id = Guid.NewGuid();
        Type = type;
        BankAccountId = bankAccountId;
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