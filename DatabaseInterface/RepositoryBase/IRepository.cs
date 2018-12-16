using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DatabaseInterface
{
    public interface IRepository<TObject> : IDisposable where TObject : class
    {
        IQueryable<TObject> GetAll();

        IQueryable<TObject> Find(Expression<Func<TObject, bool>> predicate);

        TObject Single(Expression<Func<TObject, bool>> predicate);

        TObject First(Expression<Func<TObject, bool>> predicate);

        TObject LastOrDefault(Expression<Func<TObject, bool>> predicate);

        void Add(TObject entity);

        void Delete(TObject entity);

        void Delete(Expression<Func<TObject, bool>> predicate);

        void Edit(TObject entity);

        void Save();
        DbContextTransaction BeginTransaction();
    }
}