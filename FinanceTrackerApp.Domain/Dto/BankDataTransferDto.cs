using FinanceTrackerApp.Domain.Entities;
namespace FinanceTrackerApp.Domain.Dto;

public struct BankDataTransferDto
{
    public List<BankAccount> BankAccounts { get;  set; } 
    public List<Category> Categories { get;  set; } 
    public List<Operation> Operations { get;  set; }
}