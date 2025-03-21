using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Repository;
using FinanceTrackerApp.Domain.Abstractions.Repository;

namespace FinanceTrackerApp.Domain.Patterns.Proxy;

public class BankRepositoryProxy: IBankAccountRepository, IProxy
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly Dictionary<Guid, BankAccount> _bankAccountsCache = new Dictionary<Guid, BankAccount>();

    public BankRepositoryProxy(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
        LoadCache();
    }
    public void Add(BankAccount entity)
    {
        _bankAccountRepository.Add(entity);
        _bankAccountsCache[entity.Id] = entity;
    }

    public void Update(BankAccount entity)
    {
        _bankAccountRepository.Update(entity);
        _bankAccountsCache[entity.Id] = entity;
    }

    public void Delete(Guid id)
    {
        _bankAccountRepository.Delete(id);
        _bankAccountsCache.Remove(id);
    }

    public BankAccount GetById(Guid id)
    {
        return _bankAccountsCache[id];
    }

    public IEnumerable<BankAccount> GetAll()
    {
        return _bankAccountsCache.Values;
    }

    public void LoadCache()
    {
        var bankAccounts = _bankAccountRepository.GetAll();
        foreach (var account in bankAccounts)
        {
            _bankAccountsCache[account.Id] = account;
        }
    }
}