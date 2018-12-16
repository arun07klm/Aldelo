using Aldelo.DatabaseInterface.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DatabaseInterface
{
    public abstract class RepositoryBase<TObject> : IRepository<TObject> where TObject : class
    {
        protected AldeloEntities _DbContext;

        protected IDbSet<TObject> _DbSet;

        public RepositoryBase()
        {
            _DbContext = new AldeloEntities();
            _DbSet = _DbContext.Set<TObject>();
        }

        #region IRepository<T> Members

        public virtual IQueryable<TObject> GetAll()
        {
            IQueryable<TObject> query = _DbSet.AsQueryable();
            return query;
        }

        public async Task<ICollection<TObject>> GetAllAsync()
        {
            return await _DbContext.Set<TObject>().ToListAsync();
        }
 

        public virtual IQueryable<TObject> Find(Expression<Func<TObject, bool>> predicate)
        {
            IQueryable<TObject> query = _DbSet.Where(predicate);
            return query;
        }

        public async Task<ICollection<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match)
        {
            return await _DbContext.Set<TObject>().Where(match).ToListAsync();
        }
 
        public virtual TObject Single(Expression<Func<TObject, bool>> predicate)
        {
            TObject query = _DbSet.Single(predicate);
            return query;
        }

        public TObject Get(int id)
        {
            return _DbContext.Set<TObject>().Find(id);
        }

        public async Task<TObject> GetAsync(int id)
        {
            return await _DbContext.Set<TObject>().FindAsync(id);
        }

        public virtual TObject First(Expression<Func<TObject, bool>> predicate)
        {
            TObject query = _DbSet.First(predicate);
            return query;
        }

        public virtual TObject Last(Expression<Func<TObject, bool>> predicate)
        {
            TObject query = _DbSet.Last(predicate);
            return query;
        }

        public virtual TObject LastOrDefault(Expression<Func<TObject, bool>> predicate)
        {
            TObject query = _DbSet.LastOrDefault(predicate);
            return query;
        }

        public virtual TObject FirstOrDefault(Expression<Func<TObject, bool>> predicate)
        {
            TObject query = _DbSet.FirstOrDefault(predicate);
            return query;
        }

        public virtual void Add(TObject entity)
        {
            _DbSet.Add(entity);
        }

        public async Task<TObject> AddAsync(TObject t)
        {
            _DbContext.Set<TObject>().Add(t);
            await _DbContext.SaveChangesAsync();
            return t;
        }

        public virtual void Delete(TObject entity)
        {
            _DbSet.Remove(entity);
            _DbContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<TObject, bool>> predicate)
        {
            IQueryable<TObject> records = from x in _DbSet.Where(predicate) select x;
            foreach (TObject record in records)
            {
                _DbSet.Remove(record);
            }

            _DbContext.SaveChanges();
        }

        public async Task<int> DeleteAsync(TObject t)
        {
            _DbContext.Set<TObject>().Remove(t);
            return await _DbContext.SaveChangesAsync();
        }

        public TObject Update(TObject updated, int key)
        {
            if (updated == null)
                return null;

            TObject existing = _DbContext.Set<TObject>().Find(key);
            if (existing != null)
            {
                _DbContext.Entry(existing).CurrentValues.SetValues(updated);
                _DbContext.SaveChanges();
            }
            return existing;
        }

        public async Task<TObject> UpdateAsync(TObject updated, int key)
        {
            if (updated == null)
                return null;

            TObject existing = await _DbContext.Set<TObject>().FindAsync(key);
            if (existing != null)
            {
                _DbContext.Entry(existing).CurrentValues.SetValues(updated);
                await _DbContext.SaveChangesAsync();
            }
            return existing;
        }

        public virtual void Edit(TObject entity)
        {
            // Nothing to do. Entity frame work track the changes
        }

        public virtual void Save()
        {
            _DbContext.SaveChanges();
        }    
        public void Dispose()
        {           
            _DbContext.Dispose();
            _DbContext = null;
            GC.SuppressFinalize(this);
        }

        #endregion

        public virtual int GetRecordCount()
        {
            return _DbSet.Count();
        }

        public virtual int GetRecordCount(Expression<Func<TObject, bool>> predicate)
        {
            return _DbSet.Count(predicate);
        }

        public async Task<int> GetRecordCountAsync()
        {
            return await _DbContext.Set<TObject>().CountAsync();
        }

        public virtual IEnumerable<TObject> GetDistinct(IEqualityComparer<TObject> Comparer)
        {
            IEnumerable<TObject> query = _DbSet.AsEnumerable().Distinct(Comparer);
            return query;
        }

        public virtual IEnumerable<TObject> FindDistinct(Expression<Func<TObject, bool>> predicate, IEqualityComparer<TObject> Comparer)
        {
            IEnumerable<TObject> query = _DbSet.Where(predicate).AsEnumerable().Distinct(Comparer);
            return query;
        }

        public virtual void SetLazyLoading(bool bValue)
        {
            _DbContext.Configuration.LazyLoadingEnabled = bValue;
        }
        public virtual DbContextTransaction BeginTransaction()
        {
            return _DbContext.Database.BeginTransaction();
        }

    }
}