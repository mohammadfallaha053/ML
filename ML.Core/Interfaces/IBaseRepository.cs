using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Interfaces
{
    public interface IBaseRepository <T>where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync(string[] includes = null);

        public Task<T> Find(Expression<Func<T, bool>> match, string[] includes = null);

        public  Task<T> Findcompositekey(int match, int match2);

        public  Task<T> Add(T entity);

        public  T Update(T entity);

        public T Delete(T entity);

    }
}
