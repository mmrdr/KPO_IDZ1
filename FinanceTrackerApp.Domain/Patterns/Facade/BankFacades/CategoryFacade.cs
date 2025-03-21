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

    public void CreateFromFile(Category category)
    {
        try
        {
            var e = _categoryRepository.GetById(category.Id);
            _categoryRepository.Update(e);
        }
        catch (Exception e)
        {
            _categoryRepository.Add(category);
        }
    }

    public void ChangeName(Guid id, string newName)
    {
        try
        {
            var category = _categoryRepository.GetById(id);
            category.ChangeName(newName);
            _categoryRepository.Update(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public IEnumerable<Category> GetAll()
    {
        return _categoryRepository.GetAll();
    }

    public Category? GetById(Guid id)
    {
        try
        {
            return _categoryRepository.GetById(id);
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
            _categoryRepository.Delete(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}