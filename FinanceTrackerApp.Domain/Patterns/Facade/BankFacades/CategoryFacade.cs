using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Patterns.Factory;
using FinanceTrackerApp.Domain.Repository;

namespace FinanceTrackerApp.Domain.Patterns.Facade;

public class CategoryFacade: ICategoryFacade
{
    private readonly ICategoryFactory _factory;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryFacade(ICategoryFactory factory, ICategoryRepository categoryRepository)
    {
        _factory = factory;
        _categoryRepository = categoryRepository;
    }
    
    public Category CreateCategory(OperationType type, string name)
    {
        Category category = _factory.Create(type, name);
        _categoryRepository.Add(category);
        return category;
    }

    public void ChangeName(Guid id, string newName)
    {
        var category = _categoryRepository.GetById(id);
        if (category == null)
        {
            throw new ArgumentException("Category not found");
        }
        category.ChangeName(newName);
        _categoryRepository.Update(category);
    }

    public IEnumerable<Category> GetAll()
    {
        return _categoryRepository.GetAll();
    }

    public Category? GetById(Guid id)
    {
        return _categoryRepository.GetById(id);
    }

    public void Delete(Guid id)
    {
        _categoryRepository.Delete(id);
    }
}