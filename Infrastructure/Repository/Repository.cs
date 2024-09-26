using Application.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly AppDbContext _context;
        public Repository(AppDbContext appDbContext)
        {
           _context = appDbContext;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Update(T entity)
        {
             _context.Set<T>().Update(entity);
            return true;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            return true;
        }

    }
}
