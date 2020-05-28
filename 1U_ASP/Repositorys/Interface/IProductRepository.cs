using Dap1U.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1U_ASP.Repositorys.Interface
{
  public interface IProductRepository 
  {
      Task<Product> GetProduct(int id);
      Task<List<Product>> GetAllProducts();
      Product AddProduct(Product product);
  }
}
