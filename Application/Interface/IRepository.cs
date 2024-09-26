using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(Guid id);
        IQueryable<T> GetQueryable();
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(Guid id);

    }
}
