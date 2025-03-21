using FinanceTrackerApp.Domain.Dto;
using FinanceTrackerApp.Domain.Entities;
using YamlDotNet.Serialization;

namespace FinanceTrackerApp.Domain.Patterns.Visitor;

public class YamlVisitor: IEntityVisitor, IVisitorAction
{
    private readonly List<BankAccount> _bankAccounts = new List<BankAccount>();
    private readonly List<Category> _categories = new List<Category>();
    private readonly List<Operation> _operations = new List<Operation>();
    public void Visit(BankAccount bankAccount)
    {
        _bankAccounts.Add(bankAccount);
    }

    public void Visit(Category category)
    {
        _categories.Add(category);
    }

    public void Visit(Operation operation)
    {
        _operations.Add(operation);
    }

    public string GetResult()
    {
        var dto = new BankDataTransferDto
        {
            BankAccounts = _bankAccounts,
            Categories = _categories,
            Operations = _operations,
        };
        
        var s = new SerializerBuilder();
        return s.Build().Serialize(dto);
    }
}