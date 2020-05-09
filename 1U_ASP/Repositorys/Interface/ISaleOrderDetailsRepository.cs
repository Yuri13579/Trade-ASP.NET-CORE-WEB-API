using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Models;

namespace _1U_ASP.Repositorys.Interface
{
    public interface ISaleOrderDetailsRepository
    {
        Task<List<SaleOrderDetail>> GetAllSaleOrderDetails();
    }
}
