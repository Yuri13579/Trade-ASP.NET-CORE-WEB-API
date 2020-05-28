using System.Collections.Generic;
using System.Threading.Tasks;
using _1U_ASP.Models;

namespace _1U_ASP.Repositorys.Interface
{
    public interface ISaleOrderRepository 
    {
        Task<SaleOrder> GetSaleOrders(int id);
        Task<List<SaleOrder>> GetAllSaleOrders();
    }
}
