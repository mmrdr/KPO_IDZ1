using System.Text.Json;
using FinanceTrackerApp.Domain.Dto;
using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Patterns.Visitor;

public class JsonVisitor: IEntityVisitor, IVisitorAction
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
        BankDataTransferDto model = new BankDataTransferDto
        {
            BankAccounts = _bankAccounts,
            Categories = _categories,
            Operations = _operations,
        };
        
        var options = new JsonSerializerOptions
        { WriteIndented = true };
        return JsonSerializer.Serialize(model, options);
    }
}