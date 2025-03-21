using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Patterns.Visitor;
using FinanceTrackerApp.Domain.Abstractions.Export;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using FinanceTrackerApp.Domain.Dto;

namespace FinanceTrackerApp.Domain.Export;

public class CsvExporter: DataExporter
{
    protected override string SerializeData(BankDataTransferDto data)
    {
        var visitor = new CsvVisitor();

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