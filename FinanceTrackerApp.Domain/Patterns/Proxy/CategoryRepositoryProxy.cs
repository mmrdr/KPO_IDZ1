using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Repository;
using FinanceTrackerApp.Domain.Abstractions.Repository;

namespace FinanceTrackerApp.Domain.Patterns.Proxy;

public class CategoryRepositoryProxy: ICategoryRepository, IProxy
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly Dictionary<Guid, Category> _categoriesCache = new Dictionary<Guid, Category>();

    public CategoryRepositoryProxy(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        LoadCache();
    }
    public void Add(Category entity)
    {
        _categoryRepository.Add(entity);
        _categoriesCache[entity.Id] = entity;
    }

    public void Update(Category entity)
    {
        _categoryRepository.Update(entity);
        _categoriesCache[entity.Id] = entity;
    }

    public void Delete(Guid id)
    {
        _categoryRepository.Delete(id);
        _categoriesCache.Remove(id);
    }

    public Category GetById(Guid id)
    {
        return _categoriesCache[id];
    }

    public IEnumerable<Category> GetAll()
    {
        return _categoriesCache.Values;
    }

    public void LoadCache()
    {
        var categories = _categoryRepository.GetAll();
        foreach (var category in categories)
        {
            _categoriesCache[category.Id] = category;
        }
    }
}