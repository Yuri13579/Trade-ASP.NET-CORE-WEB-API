﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Repositorys.Interface
{
  public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
         Task<List<T>> GetAllAsync();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
