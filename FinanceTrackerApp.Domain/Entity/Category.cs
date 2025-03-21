using FinanceTrackerApp.Domain.Patterns.Visitor;

namespace FinanceTrackerApp.Domain.Entities;

public class Category: IEntityVisitable, IStorable
{
    public Guid Id { get; private set; }
    public OperationType Type { get; private set; }
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