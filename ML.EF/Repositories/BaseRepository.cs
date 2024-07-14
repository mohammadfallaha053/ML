using Microsoft.EntityFrameworkCore;
using ML.Core.Interfaces;
using ML.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ML.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {   
            _context = context; 
        }

        public async Task<T> Add(T entity)
        {
             _context.Set<T>().AddAsync(entity);
             return  entity;  
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
                _context.Set<T>().AddRange(entities);
                 return entities;
        }
        


        public async Task<IEnumerable<T>> GetAllAsync(string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null) foreach (var include in includes) query = query.Include(include);
            return await query.ToListAsync();

        }




        public  T Update(T entity)
        {
                _context.Update(entity);
                 return  entity;
        }



        public  T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }


        public async Task<T> Find(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null) foreach (var include in includes) query = query.Include(include);
            return await query.SingleOrDefaultAsync(match);

        }

        public async Task<T> Findcompositekey(int match,int match2)
        {
            return await _context.Set<T>().FindAsync(match,match2);
        }

        
    }
}
