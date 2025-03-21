using FinanceTrackerApp.Domain.Abstractions.Import;
using FinanceTrackerApp.Domain.Dto;
using YamlDotNet.Serialization;

namespace FinanceTrackerApp.Domain.Import;

public class YamlDataImporter: DataImporter
{
    protected override BankDataTransferDto DeserializeData(string data)
    {
        var deserializer = new DeserializerBuilder().Build();
        return deserializer.Deserialize<BankDataTransferDto>(data);
    }
}