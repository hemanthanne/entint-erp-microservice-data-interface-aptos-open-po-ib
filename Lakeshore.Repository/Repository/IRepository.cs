using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lakeshore.DataInterface.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool enableTracking = true,
           bool ignoreQueryFilters = false);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
          bool enableTracking = true,
          bool ignoreQueryFilters = false);

        Task<IList<T>> GetAllAsync(
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool enableTracking = true,
           bool ignoreQueryFilters = false,
           CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool enableTracking = true,
           bool ignoreQueryFilters = false);

        Task<IList<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true, 
            bool ignoreQueryFilters = false, 
            CancellationToken cancellationToken = default);

        ValueTask<EntityEntry<T>> InsertAsync(T entity,
            CancellationToken cancellationToken = default);

        Task InsertAsync(params T[] entities);

        Task InsertAsync(IEnumerable<T> entities,
            CancellationToken cancellationToken = default);


        void Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);
    }
}
