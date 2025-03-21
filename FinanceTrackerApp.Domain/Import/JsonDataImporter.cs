using System.Text.Json;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Abstractions.Import;
using FinanceTrackerApp.Domain.Dto;

namespace FinanceTrackerApp.Domain.Import;

public class JsonDataImporter: DataImporter
{
    protected override BankDataTransferDto DeserializeData(string data)
    {
        return JsonSerializer.Deserialize<BankDataTransferDto>(data);
    }
}