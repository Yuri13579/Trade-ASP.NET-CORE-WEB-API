using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Models;
using Dap1U.Models;

namespace _1U_ASP.Repositorys.Interface
{
    public interface ISaleOrderRepository 
    {
        Task<SaleOrder> GetSaleOrders(int id);
        Task<List<SaleOrder>> GetAllSaleOrders();
    }
}
