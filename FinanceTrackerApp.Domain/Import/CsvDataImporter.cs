using CsvHelper;
using FinanceTrackerApp.Domain.Abstractions.Import;
using FinanceTrackerApp.Domain.Entities;
using System.Globalization;
using FinanceTrackerApp.Domain.Dto;

namespace FinanceTrackerApp.Domain.Import;

public class CsvDataImporter: DataImporter
{
    protected override BankDataTransferDto DeserializeData(string data)
    {
        var bankDto = new BankDataTransferDto();
        // проверяем что содержит заголовок чтобы понимать какой тип данных десериализуем
        using (var reader = new StringReader(data))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();
            var headers = csv.HeaderRecord;
            if (headers.Contains("Name") && headers.Contains("Balance"))
            {
                bankDto.BankAccounts = csv.GetRecords<BankAccount>().ToList();
            }
            else if (headers.Contains("Type") && headers.Contains("Name"))
            {
                bankDto.Categories = csv.GetRecords<Category>().ToList();
            }
            else if (headers.Contains("Amount") && headers.Contains("BankAccountId"))
            {
                bankDto.Operations = csv.GetRecords<Operation>().ToList();
            }
            else
            {
                throw new InvalidDataException("Unknown CSV format");
            }
        }
        return bankDto;
    }
}