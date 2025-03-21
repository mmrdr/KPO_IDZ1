using FinanceTrackerApp.Domain.Db;
using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Repository;

namespace FinanceTrackerApp.Domain.Repository;

public class CategoryRepository: ICategoryRepository
{
    private readonly FinanceAppDbContext _context;

    public CategoryRepository(FinanceAppDbContext context)
    {
        _context = context;
    }
    public void Add(Category entity)
    {
        if (_context.Categories.Find(entity.Id) != null)
        {
            Update(entity);
        }
        else
        {
            _context.Categories.Add(entity);
        }
        _context.SaveChanges();
    }

    public void Update(Category entity)
    {
        _context.Categories.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var entity = _context.Categories.Find(id);
        if (entity != null)
        {
            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }
    }

    public Category? GetById(Guid id)
    {
        return _context.Categories.Find(id);
    }

    public IEnumerable<Category> GetAll()
    {
        return _context.Categories.ToList();
    }
}