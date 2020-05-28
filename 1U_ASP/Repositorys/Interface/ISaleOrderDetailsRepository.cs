using System.Collections.Generic;
using System.Threading.Tasks;
using _1U_ASP.Models;

namespace _1U_ASP.Repositorys.Interface
{
    public interface ISaleOrderDetailsRepository
    {
        Task<List<SaleOrderDetail>> GetAllSaleOrderDetails();
    }
}
