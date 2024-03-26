using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Model.SeedWork
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        T GetById(object id);

        IQueryable<T> Get(Expression<Func<T, bool>> filter = null, string includes = "");

        void Add(T entity);

        void Remove(T entity);

        void Save();
    }
}