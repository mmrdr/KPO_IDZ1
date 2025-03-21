namespace FinanceTrackerApp.Domain.Patterns.Facade.DataTransferFacade;

public interface IDataTransferFacade
{
    public void ExportData(string filePath);
    public void ImportData(string filePath);
}