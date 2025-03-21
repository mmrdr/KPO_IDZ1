using FinanceTrackerApp.Domain.Entities;

namespace FinanceTrackerApp.Domain.Abstractions.Facade;

public interface IFacade<T> where T: IStorable
{
    IEnumerable<T> GetAll();
    T? GetById(Guid id);
    void Delete(Guid id);
}