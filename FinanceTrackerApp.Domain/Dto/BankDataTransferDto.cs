using FinanceTrackerApp.Domain.Entities;
using YamlDotNet.Serialization;
namespace FinanceTrackerApp.Domain.Dto;

public class BankDataTransferDto
{
    
    public List<BankAccount> BankAccounts { get;  set; } = new List<BankAccount>();
    
    public List<Category> Categories { get;  set; } = new List<Category>();
    
    public List<Operation> Operations { get;  set; } = new List<Operation>();
}