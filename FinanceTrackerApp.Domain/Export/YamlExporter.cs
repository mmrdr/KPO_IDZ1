using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Patterns.Visitor;
using YamlDotNet.Serialization;
using FinanceTrackerApp.Domain.Abstractions.Export;
using YamlDotNet.Serialization.NamingConventions;
using System.Text;
using FinanceTrackerApp.Domain.Dto;

namespace FinanceTrackerApp.Domain.Export;

public class YamlExporter: DataExporter
{
    protected override string SerializeData(BankDataTransferDto data)
    {
        var visitor = new YamlVisitor();

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