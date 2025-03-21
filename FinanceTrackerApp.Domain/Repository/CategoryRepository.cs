using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Repository;

namespace FinanceTrackerApp.Domain.Repositories;

public class CategoryRepository: ICategoryRepository
{
    private readonly Dictionary<Guid, Category> _categories = new Dictionary<Guid, Category>();
    public void Add(Category entity)
    {
        if (!_categories.TryAdd(entity.Id, entity))
        {
            throw new ArgumentException("Category already exists");
        }
    }

    public void Update(Category entity)
    {
        if (!_categories.ContainsKey(entity.Id))
        {
            throw new ArgumentException("Category does not exist");
        }
        _categories[entity.Id] = entity; 
    }

    public void Delete(Guid id)
    {
        if (!_categories.ContainsKey(id))
        {
            throw new ArgumentException("Category does not exist");
        }
        _categories.Remove(id);
    }

    public Category GetById(Guid id)
    {
        if (!_categories.TryGetValue(id, out var entity))
        {
            throw new ArgumentException("Category does not exist");
        }
        return entity;
    }

    public IEnumerable<Category> GetAll()
    {
        if (!_categories.Any())
        {
            throw new ArgumentException("No categories found");
        }
        return _categories.Values;
    }
}