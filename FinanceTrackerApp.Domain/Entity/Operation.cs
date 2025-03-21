using FinanceTrackerApp.Domain.Patterns.Visitor;
using CsvHelper.Configuration.Attributes;
namespace FinanceTrackerApp.Domain.Entities;

public class Operation: IEntityVisitable, IStorable
{
    [Name("Id")]
    public Guid Id { get; private set; }
    [Name("Type")]
    public OperationType Type { get; private set; }
    [Name("BankAccountId")]
    public Guid? BankAccountId { get; private set; }
    [Name("Amount")]
    public decimal Amount { get; private set; }
    [Name("Date")]
    public DateTime Date { get; private set; }
    [Name("Description")]
    public string? Description { get; private set; }
    [Name("CategoryId")]
    public Guid? CategoryId { get; private set; }

    public Operation(OperationType type, Guid? bankAccountId, decimal amount,
        string? description = null, Guid? categoryId = null)
    {
        Id = Guid.NewGuid();
        Type = type;
        BankAccountId = bankAccountId;
        Amount = amount;
        Date = DateTime.UtcNow;
        Description = description;
        CategoryId = categoryId;
    }
    
    public void Accept(IEntityVisitor visitor)
    {
        visitor.Visit(this);
    }
}