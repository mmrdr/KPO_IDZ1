using System.Globalization;
using System.Text;
using CsvHelper;
using FinanceTrackerApp.Domain.Dto;
using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Patterns.Visitor;

public class CsvVisitor: IEntityVisitor, IVisitorAction
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
        // writeRecords принимает только IEnumerable :(
        var stringBuilder = new StringBuilder();
        
        using (var writer = new StringWriter(stringBuilder))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(_bankAccounts);
            writer.WriteLine();
        }
        
        using (var writer = new StringWriter(stringBuilder))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(_categories);
            writer.WriteLine();
        }
        
        using (var writer = new StringWriter(stringBuilder))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(_operations);
        }

        return stringBuilder.ToString();
    }
}