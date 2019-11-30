using Dap1U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.MiddleTier.Interface
{
    public interface IProductService
    {
        Task<Product> GetProductById(int id);
    }
}
