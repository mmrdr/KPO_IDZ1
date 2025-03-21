using System.Collections;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Repository;

namespace FinanceTrackerApp.Domain.Repositories;

public class BankAccountRepository: IBankAccountRepository
{
    private readonly Dictionary<Guid, BankAccount> _bankAccounts = new Dictionary<Guid, BankAccount>();
    
    public void Add(BankAccount entity)
    {
        if (!_bankAccounts.TryAdd(entity.Id, entity))
        {
            throw new ArgumentException("Bank account already exists.");
        }
    }

    public void Update(BankAccount entity)
    {
        if (!_bankAccounts.ContainsKey(entity.Id))
        {
            throw new ArgumentException("Bank account does not exist.");
        }
        _bankAccounts[entity.Id] = entity;
    }

    public void Delete(Guid id)
    {
        if (!_bankAccounts.ContainsKey(id))
        {
            throw new ArgumentException("Bank account does not exist.");
        }
        _bankAccounts.Remove(id);
    }

    public BankAccount GetById(Guid id)
    {
        if (!_bankAccounts.TryGetValue(id, out var bankAccount))
        {
            throw new ArgumentException("Bank account does not exist.");
        }
        return bankAccount;
    }

    public IEnumerable<BankAccount> GetAll()
    {
        if (!_bankAccounts.Any())
        {
            throw new ArgumentException("Bank account is empty.");
        }
        return _bankAccounts.Values;
    }
}