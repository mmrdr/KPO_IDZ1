using FinanceTrackerApp.Domain.Abstractions.Repository;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Patterns.Factory;
using FinanceTrackerApp.Domain.Repository;

namespace FinanceTrackerApp.Domain.Patterns.Facade;

public class BankAccountFacade: IBankAccountFacade
{
    private readonly IBankAccountFactory _factory;
    private readonly IBankAccountRepository _bankAccountRepository;

    public BankAccountFacade(IBankAccountFactory factory, IBankAccountRepository bankAccountRepository)
    {
        _factory = factory;
        _bankAccountRepository = bankAccountRepository;
    }

    public BankAccount CreateAccount(string name, decimal balance)
    {
        BankAccount bankAccount = _factory.Create(name, balance);
        _bankAccountRepository.Add(bankAccount);
        return bankAccount;
    }

    public void CreateFromFile(BankAccount account)
    {
        var e = _bankAccountRepository.GetById(account.Id);
        if (e != null)
        {
            _bankAccountRepository.Update(e);
        }
        else
        {
            _bankAccountRepository.Add(account);
        }
    }

    public void ChangeName(Guid id, string name)
    {
        var account = _bankAccountRepository.GetById(id);
        if (account == null)
        {
            throw new ArgumentException("Account not found");
        }
        account.ChangeName(name);
        _bankAccountRepository.Update(account);
    }

    public void IncreaseBalance(Guid id, decimal amount)
    {
        var account = _bankAccountRepository.GetById(id);
        if (account == null)
        {
            throw new ArgumentException("Account not found");
        }
        account.IncreaseBalance(amount);
        _bankAccountRepository.Update(account);
    }

    public void DecreaseBalance(Guid id, decimal amount)
    {
        var account = _bankAccountRepository.GetById(id);
        if (account == null)
        {
            throw new ArgumentException("Account not found");
        }
        account.DecreaseBalance(amount);
        _bankAccountRepository.Update(account);
    }

    public IEnumerable<BankAccount> GetAll()
    {
        return _bankAccountRepository.GetAll();
    }

    public BankAccount? GetById(Guid id)
    {
        return _bankAccountRepository.GetById(id);
    }

    public void Delete(Guid id)
    {
        _bankAccountRepository.Delete(id);
    }
}