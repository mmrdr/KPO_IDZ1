using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Patterns.Visitor;

public interface IEntityVisitor
{
    void Visit(BankAccount bankAccount);
    void Visit(Category category);
    void Visit(Operation operation);
}