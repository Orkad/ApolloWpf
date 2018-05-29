using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApolloWpfCore.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Query(Expression<Func<T, bool>> filter, params string[] includes);
        T GetOne(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}