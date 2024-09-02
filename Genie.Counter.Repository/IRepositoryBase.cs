using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genie.Counter.Repository;
public interface IRepositoryBase<T> where T : class
{
    Task<IEnumerable<T>> GetFilteredAsync(Dictionary<string, string> filters);
    Task<T> GetByPrimaryKeyAsync(object keyValue);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(object keyValue);
    object GetPrimaryKeyValue(T entity);
}
