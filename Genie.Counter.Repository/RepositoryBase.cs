using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Genie.Counter.DBContext;

namespace Genie.Counter.Repository;
public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetFilteredAsync(Dictionary<string, object> filters)
    {
        var query = _dbSet.AsQueryable();
        query = ApplyFilters(query, filters);
        return await query.ToListAsync();
    }

    public async Task<T> GetByPrimaryKeyAsync(object keyValue)
    {
        var keyProperty = GetKeyProperty();

        var entity = await _dbSet.FindAsync(keyValue);
        return entity ?? throw new Exception("Record Not FOund");
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(object keyValue)
    {
        var entity = await GetByPrimaryKeyAsync(keyValue);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    private IQueryable<T> ApplyFilters(IQueryable<T> query, Dictionary<string, object> filters)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        foreach (var filter in filters)
        {
            var property = typeof(T).GetProperty(filter.Key);
            if (property == null) continue;

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var constant = Expression.Constant(filter.Value);
            var equals = Expression.Equal(propertyAccess, constant);

            var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);
            query = query.Where(lambda);
        }

        return query;
    }

    public object GetPrimaryKeyValue(T entity)
    {
        var keyProperty = GetKeyProperty();
        if (keyProperty == null)
        {
            throw new InvalidOperationException("No key property defined for the entity.");
        }

        return keyProperty.GetValue(entity) ?? throw new InvalidOperationException("Invalid property defined for the entity.");
    }
    private PropertyInfo GetKeyProperty()
    {
        var keyProperties = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties;
        return keyProperties?.FirstOrDefault()?.PropertyInfo?? throw new InvalidOperationException("No key property defined for the entity.");
    }
}
