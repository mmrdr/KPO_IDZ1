using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Patterns.Factory;
using FinanceTrackerApp.Domain.Repository;

namespace FinanceTrackerApp.Domain.Patterns.Facade;

public class OperationFacade: IOperationFacade
{
    private readonly IOperationFactory _operationFactory;
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOperationRepository _operationRepository;

    public OperationFacade(IOperationFactory operationFactory, IBankAccountRepository bankAccountRepository,
        ICategoryRepository categoryRepository, IOperationRepository operationRepository)
    {
        _operationFactory = operationFactory;
        _bankAccountRepository = bankAccountRepository;
        _categoryRepository = categoryRepository;
        _operationRepository = operationRepository;
    }
    
    public Operation CreateOperationById(OperationType type, Guid bankAccountId, decimal amount, string? description,
        Guid? categoryId)
    {
        var operation = _operationFactory.Create(type, bankAccountId, amount, description, categoryId);
        _operationRepository.Add(operation);
        return operation;
    }

    public void CreateFromFile(Operation operation)
    {
        try
        {
            var e = _operationRepository.GetById(operation.Id);
            _operationRepository.Update(e);
        }
        catch (Exception e)
        {
            _operationRepository.Add(operation);
        }
    }

    public IEnumerable<Operation> GetAll()
    {
        return _operationRepository.GetAll();
    }

    public Operation? GetById(Guid id)
    {
        try
        {
            return _operationRepository.GetById(id);
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
            _operationRepository.Delete(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}