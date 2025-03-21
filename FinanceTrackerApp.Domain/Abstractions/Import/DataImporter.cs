using FinanceTrackerApp.Domain.Dto;

namespace FinanceTrackerApp.Domain.Abstractions.Import;

public abstract class DataImporter
{
    public BankDataTransferDto ImportData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }
        string data = File.ReadAllText(filePath);
        return DeserializeData(data);
    }
    protected abstract BankDataTransferDto DeserializeData(string data);
}