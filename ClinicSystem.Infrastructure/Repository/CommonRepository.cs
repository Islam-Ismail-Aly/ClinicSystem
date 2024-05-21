using ClinicSystem.Core.Interfaces;
using ClinicSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClinicSystem.Infrastructure.Repository
{
    public class CommonRepository<T> : ICommonRepository<T>, IDisposable where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public CommonRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<IEnumerable<T>> GetOrderedByIncludingAsync<TKey>(Expression<Func<T, TKey>> keySelector, bool ascending,
               params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = ascending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);

            return await query.ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
