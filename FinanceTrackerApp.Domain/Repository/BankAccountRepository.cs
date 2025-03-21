using System.Collections;
using FinanceTrackerApp.Domain.Db;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Repository;

namespace FinanceTrackerApp.Domain.Repository;

public class BankAccountRepository: IBankAccountRepository
{
    private readonly FinanceAppDbContext _context;

    public BankAccountRepository(FinanceAppDbContext context)
    {
        _context = context;
    }
    public void Add(BankAccount entity)
    {
        if (_context.BankAccounts.Find(entity.Id) != null)
        {
            Update(entity);
        }
        else
        {
            _context.BankAccounts.Add(entity);
        }
        _context.SaveChanges();
    }

    public void Update(BankAccount entity)
    {
        _context.BankAccounts.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var acc = _context.BankAccounts.Find(id);
        if (acc != null)
        {
            _context.BankAccounts.Remove(acc);
            _context.SaveChanges();
        }
    }

    public BankAccount? GetById(Guid id)
    {
        return _context.BankAccounts.Find(id);
    }

    public IEnumerable<BankAccount> GetAll()
    {
        return _context.BankAccounts.ToList();
    }
}