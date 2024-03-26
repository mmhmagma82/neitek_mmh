using Domain.Model.SeedWork;
using Infrastructure.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.SeedWork
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDatabaseContext _database;

        public Repository(IDatabaseContext database)
        {
            _database = database;
        }

        public IQueryable<T> GetAll()
        {
            return _database.Set<T>();
        }

        public T GetById(object id)
        {
            return _database.Set<T>().Find(id);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, string includes = "")
        {
            var query = _database.Set<T>().AsQueryable();
            if (filter != null)
                query = query.Where(filter);
            if(!string.IsNullOrEmpty(includes))
            {
                foreach(var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return _database.Set<T>();
        }

        public void Add(T entity)
        {
            _database.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _database.Set<T>().Remove(entity);
        }

        public void Save()
        {
            _database.Save();
        }
    }
}