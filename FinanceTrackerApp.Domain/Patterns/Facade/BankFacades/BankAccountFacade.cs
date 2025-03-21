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
        try
        {
            var e = _bankAccountRepository.GetById(account.Id);
            _bankAccountRepository.Update(account);
        }
        catch (Exception e)
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
        try
        {
            var account = _bankAccountRepository.GetById(id);
            account.IncreaseBalance(amount);
            _bankAccountRepository.Update(account);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void DecreaseBalance(Guid id, decimal amount)
    {
        try
        {
            var account = _bankAccountRepository.GetById(id);
            account.DecreaseBalance(amount);
            _bankAccountRepository.Update(account);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e.ParamName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public IEnumerable<BankAccount> GetAll()
    {
        return _bankAccountRepository.GetAll();
    }

    public BankAccount? GetById(Guid id)
    {
        try
        {
            return _bankAccountRepository.GetById(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void Delete(Guid id)
    {
        try
        {
            _bankAccountRepository.Delete(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}