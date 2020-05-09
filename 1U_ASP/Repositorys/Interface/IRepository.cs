using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace _1U_ASP.Repositorys.Interface
{
  public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
       // Task<T> GetBySpecAsync(ISpecification<T> spec);
        Task<T> GetBySpecAsync(Expression<Func<T, bool>> criteria);
        Task<List<T>> ListAllAsync();
        //Task<List<T>> ListAsync(ISpecification<T> spec);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria);
        Task<T> AddAsync(T entity);
        Task<T> AddAsyncWithoutSaving(T entity);
        Task UpdateAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task AddRangeAsyncWithoutSaving(List<T> entities);
        Task AddRangeAsync(IQueryable<T> entities);
        Task UpdateRangeAsync(List<T> entities);
        Task UpdateRangeAsync(IQueryable<T> entities);


        T GetById(int id);
       // T GetBySpec(ISpecification<T> spec);
        T GetBySpec(Expression<Func<T, bool>> criteria);
        IQueryable<T> ListAll();
       // IQueryable<T> List(ISpecification<T> spec);
        IQueryable<T> List(Expression<Func<T, bool>> criteria);
        Task<T> Add(T entity);
        T AddWithoutSaving(T entity);
        void Update(T entity);
        void UpdateWithoutSaving(T entity);
        void Saving();
        IDbContextTransaction BeginTransaction();
        IExecutionStrategy CreateExecutionStrategy();
        void CommitTransaction();
        void RollbackT();
        IDbContextTransaction CurrentTransaction();
        //Task SoftDeleteAsync<K>(K entity, int userActionId) where K : IBaseEntity, T;
        //Task SoftDeleteRangeAsync<K>(List<K> entities, int userActionId) where K : IBaseEntity, T;
        int ExecuteSqlCommand(string sql);
        Task DeleteAsync(T entity);
        Task DeleteAsyncById(int id);
        Task SavingAsync();
        //Task<List<T>> GetAll();
        // Task<List<T>> GetAllAsync();
        //Task<T> Get(int id);
        //Task<T> Add(T entity);
        //Task<T> Update(T entity);
        //Task<T> Delete(int id);
    }
}
