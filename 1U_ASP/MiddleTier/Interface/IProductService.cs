using Dap1U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.Models;

namespace _1U_ASP.MiddleTier.Interface
{
    public interface IProductService
    {
        Task<Product> GetProductById(int id);
        Task<List<Product>>  GetAllProducts();

        Task<bool> AddProduct(Product productDto);
        Task<bool> DeleteProductById(int id);
        Task<bool> PutProduct(Product product);
    }
}
