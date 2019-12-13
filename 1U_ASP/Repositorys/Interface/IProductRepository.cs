using Dap1U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Repositorys.Interface
{
  public interface IProductRepository : IRepository<Product>
  {
      Task<Product> GetProduct(int id);
      IQueryable<Product> GetAllProducts();
  }
}
