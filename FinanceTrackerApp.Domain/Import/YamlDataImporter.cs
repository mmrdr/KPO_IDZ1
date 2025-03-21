using FinanceTrackerApp.Domain.Abstractions.Import;
using FinanceTrackerApp.Domain.Dto;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
namespace FinanceTrackerApp.Domain.Import;

public class YamlDataImporter: DataImporter
{
    protected override BankDataTransferDto DeserializeData(string data)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();
        return deserializer.Deserialize<BankDataTransferDto>(data);
    }
}