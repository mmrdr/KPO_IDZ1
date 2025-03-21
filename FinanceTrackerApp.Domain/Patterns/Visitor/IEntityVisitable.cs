namespace FinanceTrackerApp.Domain.Patterns.Visitor;

public interface IEntityVisitable
{
    void Accept(IEntityVisitor visitor);
}