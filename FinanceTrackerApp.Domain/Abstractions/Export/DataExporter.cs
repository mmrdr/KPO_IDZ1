using FinanceTrackerApp.Domain.Dto;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Patterns.Visitor;

namespace FinanceTrackerApp.Domain.Abstractions.Export;

public abstract class DataExporter
{
    public void ExportData(string filePath, BankDataTransferDto data)
    {
        string sData = SerializeData(data);
        File.WriteAllText(filePath, sData);
    }
    protected abstract string SerializeData(BankDataTransferDto data);
}