using FinanceTrackerApp.Domain.Db;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Repository;

namespace FinanceTrackerApp.Domain.Repository;

public class OperationRepository: IOperationRepository
{
    private readonly FinanceAppDbContext _context;

    public OperationRepository(FinanceAppDbContext context)
    {
        _context = context;
    }
    public void Add(Operation entity)
    {
        if (_context.Operations.Find(entity.Id) != null)
        {
            Update(entity);
        }
        else
        {
            _context.Operations.Add(entity);
        }

        _context.SaveChanges();
    }

    public void Update(Operation entity)
    {
        _context.Operations.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var operation = _context.Operations.Find(id);
        if (operation != null)
        {
            _context.Operations.Remove(operation);
            _context.SaveChanges();
        }
    }

    public Operation? GetById(Guid id)
    {
        return _context.Operations.Find(id);
    }

    public IEnumerable<Operation> GetAll()
    {
        return _context.Operations.ToList();
    }
}