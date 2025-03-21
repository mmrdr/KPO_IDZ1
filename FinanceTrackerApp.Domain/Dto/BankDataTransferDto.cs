using FinanceTrackerApp.Domain.Entities;
namespace FinanceTrackerApp.Domain.Dto;

public class BankData
{
    public List<BankAccount> BankAccounts { get; private set; } = new();
    public List<Category> Categories { get; private set; } = new();
    public List<Operation> Operations { get; private set; } = new();
}