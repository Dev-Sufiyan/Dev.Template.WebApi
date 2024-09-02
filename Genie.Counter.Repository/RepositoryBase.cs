using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Genie.Counter.DBContext;

namespace Genie.Counter.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetFilteredAsync(Dictionary<string, string> filters)
        {
            var query = _dbSet.AsQueryable();
            query = ApplyFilters(query, filters);
            return await query.ToListAsync();
        }

        public async Task<T> GetByPrimaryKeyAsync(object keyValue)
        {
            var entity = await _dbSet.FindAsync(keyValue);
            return entity ?? throw new Exception("Record Not Found");
        }

        public async Task AddAsync(T entity)
        {
            var keyValue = GetPrimaryKeyValue(entity);
            var existingEntity = await _dbSet.FindAsync(keyValue);

            if (existingEntity != null)
            {
                throw new InvalidOperationException("Entity with the same key already exists.");
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var keyValue = GetPrimaryKeyValue(entity);
            var existingEntity = await _dbSet.FindAsync(keyValue);

            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entity not found to update.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
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

        private IQueryable<T> ApplyFilters(IQueryable<T> query, Dictionary<string, string> filters)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            foreach (var filter in filters)
            {
                var property = typeof(T).GetProperty(filter.Key);
                if (property == null) continue;
                var propertyType = property.PropertyType;


                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var constant = ConvertToType(filter.Value, propertyType);
                var constantExpression = Expression.Constant(constant, propertyType);
                var equals = Expression.Equal(propertyAccess, constantExpression);

                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        private object ConvertToType(string value, Type targetType)
        {
            if (targetType == typeof(string)) return value;
            if (targetType == typeof(int)) return int.Parse(value);
            if (targetType == typeof(double)) return double.Parse(value);
            if (targetType == typeof(bool)) return bool.Parse(value);
            if (targetType == typeof(DateTime)) return DateTime.Parse(value);

            throw new NotSupportedException($"Conversion to {targetType.Name} is not supported.");
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
            return keyProperties?.FirstOrDefault()?.PropertyInfo ?? throw new InvalidOperationException("No key property defined for the entity.");
        }
    }
}
