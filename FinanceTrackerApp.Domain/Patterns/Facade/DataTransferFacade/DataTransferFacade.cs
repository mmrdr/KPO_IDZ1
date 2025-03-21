using FinanceTrackerApp.Domain.Abstractions.Export;
using FinanceTrackerApp.Domain.Abstractions.Import;
using FinanceTrackerApp.Domain.Dto;

namespace FinanceTrackerApp.Domain.Patterns.Facade.DataTransferFacade;

public class DataTransferFacade: IDataTransferFacade
{
    private readonly DataImporter _dataImporter;
    private readonly DataExporter _dataExporter;
    private readonly IBankAccountFacade _bankAccountFacade;
    private readonly ICategoryFacade _categoryFacade;
    private readonly IOperationFacade _operationFacade;

    public DataTransferFacade(
        DataImporter dataImporter,
        DataExporter dataExporter,
        IBankAccountFacade bankAccountFacade,
        ICategoryFacade categoryFacade,
        IOperationFacade operationFacade
    )
    {
        _dataImporter = dataImporter;
        _dataExporter = dataExporter;
        _bankAccountFacade = bankAccountFacade;
        _categoryFacade = categoryFacade;
        _operationFacade = operationFacade;
    }
        
    public void ExportData(string filePath)
    {
        var data = new BankDataTransferDto
        {
            BankAccounts = _bankAccountFacade.GetAll().ToList(),
            Categories = _categoryFacade.GetAll().ToList(),
            Operations = _operationFacade.GetAll().ToList(),
        };
        _dataExporter.ExportData(filePath, data);
    }

    public void ImportData(string filePath)
    {
        var data = _dataImporter.ImportData(filePath);
        
        foreach (var bankAccount in data.BankAccounts)
        {
            _bankAccountFacade.CreateFromFile(bankAccount);
        }

        foreach (var category in data.Categories)
        {
            _categoryFacade.CreateFromFile(category);
        }

        foreach (var operation in data.Operations)
        {
            _operationFacade.CreateFromFile(operation);
        }
    }
}