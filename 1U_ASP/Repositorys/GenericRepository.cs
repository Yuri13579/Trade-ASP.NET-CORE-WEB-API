using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _1U_ASP.Context;
using _1U_ASP.Repositorys.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace _1U_ASP.Repositorys
{
    public class GenericRepository<T> : IRepository<T>, IDisposable where T : BaseEntity
       {
        private readonly ApplicationContext _dbContext;
        private bool _disposed;

        public GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Async
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        //public async Task<T> GetBySpecAsync(ISpecification<T> spec)
        //{
        //    try
        //    {
        //        var queryableResultWithIncludes = spec.Includes
        //            .Aggregate(_dbContext.Set<T>().AsQueryable(), (current, include) => current.Include(include));

        //        var secondaryResult = spec.IncludeStrings
        //            .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

        //        return await secondaryResult.Where(spec.Criteria).FirstOrDefaultAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }
        //}

        public async Task<T> GetBySpecAsync(Expression<Func<T, bool>> criteria)
        {
            try
            {
                return await _dbContext.Set<T>().Where(criteria).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<T>> ListAllAsync()
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //public async Task<List<T>> ListAsync(ISpecification<T> spec)
        //{
        //    var queryableResultWithIncludes = spec.Includes
        //        .Aggregate(_dbContext.Set<T>().AsQueryable(), (current, include) => current.Include(include));

        //    var secondaryResult = spec.IncludeStrings
        //        .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

        //    return await secondaryResult.Where(spec.Criteria).ToListAsync();
        //}

        public Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria)
        {
            return _dbContext.Set<T>().Where(criteria).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsyncById(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
           
        }

        public async Task<T> AddAsyncWithoutSaving(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);

            return entity;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsyncWithoutSaving(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public async Task AddRangeAsync(IQueryable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            foreach (var entity in entities)
                _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IQueryable<T> entities)
        {
            foreach (var entity in entities)
                _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        //public async Task SoftDeleteAsync<K>(K entity, int userActionId) where K : T, IBaseEntity
        //{
        //    if (entity == null)
        //        return;

        //    entity.Deleted = true;
        //    entity.UserActionId = userActionId;

        //    await UpdateAsync(entity);
        //}

        //public async Task SoftDeleteRangeAsync<K>(List<K> entities, int userActionId) where K : T, IBaseEntity
        //{
        //    if (entities == null
        //        || entities.Count() == 0)
        //        return;

        //    foreach (var entity in entities)
        //    {
        //        entity.Deleted = true;
        //        entity.UserActionId = userActionId;
        //        _dbContext.Entry(entity).State = EntityState.Modified;
        //    }

        //    await _dbContext.SaveChangesAsync();
        //}

        public async Task SavingAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        //public T GetBySpec(ISpecification<T> spec)
        //{
        //    // fetch a Queryable that includes all expression-based includes
        //    var queryableResultWithIncludes = spec.Includes
        //        .Aggregate(_dbContext.Set<T>().AsQueryable(), (current, include) => current.Include(include));

        //    // modify the IQueryable to include any string-based include statements
        //    var secondaryResult = spec.IncludeStrings
        //        .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

        //    // return the result of the query using the specification's criteria expression
        //    return secondaryResult.Where(spec.Criteria).FirstOrDefault();
        //}

        public T GetBySpec(Expression<Func<T, bool>> criteria)
        {
            try
            {
                return _dbContext.Set<T>().Where(criteria).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public IQueryable<T> ListAll()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        //public IQueryable<T> List(ISpecification<T> spec)
        //{
        //    var queryableResultWithIncludes = spec.Includes
        //        .Aggregate(_dbContext.Set<T>().AsQueryable(), (current, include) => current.Include(include));

        //    var secondaryResult = spec.IncludeStrings
        //        .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

        //    return secondaryResult.Where(spec.Criteria).AsQueryable();
        //}

        public IQueryable<T> List(Expression<Func<T, bool>> criteria)
        {
            try
            {
                return _dbContext.Set<T>().Where(criteria).AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<T> Add(T entity)
        {
           await _dbContext.Set<T>().AddAsync(entity);
           await _dbContext.SaveChangesAsync();

            return entity;
        }

        public T AddWithoutSaving(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void UpdateWithoutSaving(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Saving()
        {
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                   // _dbContext = null;
                }

            _disposed = true;
        }

        ~GenericRepository()
        {
            Dispose(false);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _dbContext.Database.CreateExecutionStrategy();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollbackT()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public IDbContextTransaction CurrentTransaction()
        {
            return _dbContext.Database.CurrentTransaction;
        }

        public int ExecuteSqlCommand(string sql)
        {
            return _dbContext.Database.ExecuteSqlCommand(sql);
        }
        
    }
}
