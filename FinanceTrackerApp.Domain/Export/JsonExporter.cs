using System.Text;
using System.Text.Json;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Abstractions.Export;
using FinanceTrackerApp.Domain.Dto;
using FinanceTrackerApp.Domain.Patterns.Visitor;

namespace FinanceTrackerApp.Domain.Export;

public class JsonExporter: DataExporter
{
    protected override string SerializeData(BankDataTransferDto data)
    {
        var visitor = new JsonVisitor();
        foreach (var entity in data.BankAccounts)
        {
            entity.Accept(visitor);
        }

        foreach (var entity in data.Categories)
        {
            entity.Accept(visitor);
        }

        foreach (var entity in data.Operations)
        {
            entity.Accept(visitor);
        }

        return visitor.GetResult();
    }
}