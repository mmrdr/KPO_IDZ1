using CsvHelper;
using FinanceTrackerApp.Domain.Abstractions.Import;
using FinanceTrackerApp.Domain.Entities;
using System.Globalization;
using CsvHelper.Configuration;
using FinanceTrackerApp.Domain.Dto;

namespace FinanceTrackerApp.Domain.Import;

public class CsvDataImporter: DataImporter
{
    protected override BankDataTransferDto DeserializeData(string data)
    {
        var bankDto = new BankDataTransferDto();

        // Разделяем файл на части по пустым строкам
        var sections = data.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MissingFieldFound = null,
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };
        foreach (var section in sections)
        {
            using (var reader = new StringReader(section))
            using (var csv = new CsvReader(reader, config))
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
        }
        return bankDto;
    }
}