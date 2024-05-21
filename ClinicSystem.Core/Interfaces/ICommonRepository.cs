using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Core.Interfaces
{
    public interface ICommonRepository<T> where T : class
    {
        Task<int> CountAsync();
        Task<IEnumerable<T>> GetOrderedByIncludingAsync<TKey>(
                Expression<Func<T, TKey>> keySelector,
                bool ascending,
                params Expression<Func<T, object>>[] includes);
    }
}
