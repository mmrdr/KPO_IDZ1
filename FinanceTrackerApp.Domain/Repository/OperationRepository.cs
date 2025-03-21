using FinanceTrackerApp.Domain.Entities;
using FinanceTrackerApp.Domain.Repository;

namespace FinanceTrackerApp.Domain.Repositories;

public class OperationRepository: IOperationRepository
{
    private readonly Dictionary<Guid, Operation> _operations = new Dictionary<Guid, Operation>();
    public void Add(Operation entity)
    {
        if (!_operations.TryAdd(entity.Id, entity))
        {
            throw new ArgumentException("An operation with the same id already exists.");
        }
    }

    public void Update(Operation entity)
    {
        if (!_operations.ContainsKey(entity.Id))
        {
            throw new ArgumentException("An operation with the same id does not exist.");
        }
        _operations[entity.Id] = entity;
    }

    public void Delete(Guid id)
    {
        if (!_operations.ContainsKey(id))
        {
            throw new ArgumentException("An operation with the same id does not exist.");
        } 
        _operations.Remove(id);
    }

    public Operation GetById(Guid id)
    {
        if (!_operations.TryGetValue(id, out var entity))
        {
            throw new ArgumentException("An operation with the same id does not exist.");
        }

        return entity;
    }

    public IEnumerable<Operation> GetAll()
    {
        if (!_operations.Any())
        {
            throw new ArgumentException("No operations exist.");
        }
        return _operations.Values;
    }
}