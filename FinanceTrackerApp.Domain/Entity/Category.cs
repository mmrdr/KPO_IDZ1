using FinanceTrackerApp.Domain.Patterns.Visitor;
using CsvHelper.Configuration.Attributes;
namespace FinanceTrackerApp.Domain.Entities;

public class Category: IEntityVisitable, IStorable
{
    [Name("Id")]
    public Guid Id { get; private set; }
    [Name("Type")]
    public OperationType Type { get; private set; }
    [Name("Name")]
    public string Name { get; private set; }

    public Category(OperationType type, string name)
    {
        Id = Guid.NewGuid();
        Type = type;
        Name = name;
    }

    public void ChangeName(string newName)
    {
        Name = newName;
    }
    
    public void Accept(IEntityVisitor visitor)
    {
        visitor.Visit(this);
    }
}