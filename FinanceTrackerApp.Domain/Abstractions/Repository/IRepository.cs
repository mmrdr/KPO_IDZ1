using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Repository;

public interface IRepository<T> where T : IStorable
{
    void Add(T entity);
    void Update(T entity);
    void Delete(Guid id);
    T? GetById(Guid id);
    IEnumerable<T> GetAll();
}