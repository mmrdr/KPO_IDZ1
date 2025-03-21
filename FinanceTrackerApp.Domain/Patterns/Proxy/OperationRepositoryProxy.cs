using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Repository;
using FinanceTrackerApp.Domain.Abstractions.Repository;

namespace FinanceTrackerApp.Domain.Patterns.Proxy;

public class OperationRepositoryProxy: IOperationRepository, IProxy
{
    private readonly IRepository<Operation> _operationsRepository;
    private readonly Dictionary<Guid, Operation> _operationsCache = new Dictionary<Guid, Operation>();

    public OperationRepositoryProxy(IRepository<Operation> operationsRepository)
    {
        _operationsRepository = operationsRepository;
        LoadCache();
    }
    public void Add(Operation entity)
    {
        _operationsRepository.Add(entity);
        _operationsCache[entity.Id] = entity;
    }

    public void Update(Operation entity)
    {
        _operationsRepository.Update(entity);
        _operationsCache[entity.Id] = entity;
    }

    public void Delete(Guid id)
    {
        _operationsRepository.Delete(id);
        _operationsCache.Remove(id);
    }

    public Operation GetById(Guid id)
    {
        return _operationsCache[id];
    }

    public IEnumerable<Operation> GetAll()
    {
        return _operationsCache.Values;
    }

    public void LoadCache()
    {
        var operations = _operationsRepository.GetAll();
        foreach (var operation in operations)
        {
            _operationsCache[operation.Id] = operation;
        }
    }
}