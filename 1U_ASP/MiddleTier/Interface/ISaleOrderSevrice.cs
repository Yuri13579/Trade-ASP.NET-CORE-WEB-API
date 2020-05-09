using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1U_ASP.MiddleTier.Interface
{
    public interface ISaleOrderSevrice
    {
        Task<SaleOrder> GetSaleOrderById(int id);
        Task<List<SaleOrder>> GetAllSaleOrder();
        Task<List<SaleDTO>> GetAllSale();
        Task<string> SellGoods(SellDto sellDto);
    }
}
